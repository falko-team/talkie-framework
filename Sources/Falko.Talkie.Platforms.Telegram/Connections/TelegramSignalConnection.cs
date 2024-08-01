using System.Net;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Models;
using Talkie.Collections;
using Talkie.Concurrent;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Signals;
using Talkie.Validations;

namespace Talkie.Connections;

public sealed class TelegramSignalConnection(ISignalFlow flow,
    ServerConfiguration serverConfiguration,
    ClientConfiguration clientConfiguration) : ISignalConnection
{
    private CancellationTokenSource? _globalCancellationTokenSource;

    private Task? _processUpdatesTask;

    public TelegramPlatform? Platform { get; private set; }

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_processUpdatesTask is not null) return;

        serverConfiguration.ThrowIf().Null();
        clientConfiguration.ThrowIf().Null();

        _globalCancellationTokenSource = new CancellationTokenSource();

        var client = new TelegramBotApiClient(serverConfiguration, clientConfiguration);

        TelegramBotProfile self;

        try
        {
            self = GetSelf(await client.GetMeAsync(cancellationToken));
        }
        catch (TelegramBotApiRequestException exception)
        {
            if (exception.StatusCode is null or not HttpStatusCode.Unauthorized) throw;

            throw new UnauthorizedAccessException("Invalid token");
        }

        Platform = new TelegramPlatform(client, self);

        if (_processUpdatesTask is not null) return;

        _processUpdatesTask = ProcessUpdatesAsync(client, Platform, _globalCancellationTokenSource.Token);
    }

    private async ValueTask ProcessUpdate(TelegramPlatform platform, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;

        var incomingMessage = IncomingMessageConverter.Convert(platform, message);

        if (incomingMessage is null) return;

        try
        {
            await flow.PublishAsync(new TelegramIncomingMessageSignal(incomingMessage), cancellationToken);
        }
        catch (Exception exception)
        {
            _ = flow.PublishUnobservedPublishingExceptionAsync(exception, cancellationToken);
        }
    }

    private static TelegramBotProfile GetSelf(User self)
    {
        return new TelegramBotProfile
        {
            Identifier = self.Id,
            FirstName = self.FirstName,
            LastName = self.LastName,
            NickName = self.Username
        };
    }

    private Task ProcessUpdatesAsync(ITelegramBotApiClient client, TelegramPlatform platform,
        CancellationToken cancellationToken)
    {
        return Task.Factory.StartNew(async () =>
        {
            long offset = 0;

            while (cancellationToken.IsCancellationRequested is false)
            {
                try
                {
                    var updates = await client.GetUpdatesAsync(new GetUpdates(offset), cancellationToken);

                    if (updates.Length is 0) continue;

                    offset = updates[^1].UpdateId + 1;

                    _ = updates.ToFrozenSequence().Parallelize().ForEachAsync((update, scopedCancellationToken) =>
                            ProcessUpdate(platform, update, scopedCancellationToken), cancellationToken)
                        .ContinueWith(task =>
                            HandleProcessUpdateFaults(cancellationToken, task), TaskContinuationOptions.ExecuteSynchronously);
                }
                catch (Exception exception)
                {
                    _ = flow.PublishUnobservedConnectionExceptionAsync(this, exception, cancellationToken);
                }
            }
        }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private void HandleProcessUpdateFaults(CancellationToken cancellationToken, Task task)
    {
        if (task.IsFaulted is false || task.Exception is null) return;

        _ = flow.PublishUnobservedConnectionExceptionAsync(this, task.Exception, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_globalCancellationTokenSource is not null)
        {
            await _globalCancellationTokenSource.CancelAsync();

            _globalCancellationTokenSource.Dispose();
        }

        if (_processUpdatesTask is not null)
        {
            await _processUpdatesTask;
        }
    }
}
