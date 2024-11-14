using System.Net;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Concurrent;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Sequences;

namespace Talkie.Connections;

public sealed class TelegramSignalConnection(ISignalFlow flow,
    TelegramServerConfiguration serverConfiguration,
    TelegramClientConfiguration clientConfiguration) : SignalConnection(flow), IWithPlatformSignalConnection
{
    public TelegramPlatform? Platform { get; private set; }

    IPlatform? IWithPlatformSignalConnection.Platform => Platform;

    protected override async Task WhenInitializingAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(serverConfiguration);
        ArgumentNullException.ThrowIfNull(clientConfiguration);

        var client = new TelegramClient(new TelegramConfiguration(serverConfiguration, clientConfiguration));

        TelegramBotProfile self;

        try
        {
            self = GetSelf(await client.GetMeAsync(cancellationToken));
        }
        catch (TelegramException exception)
        {
            if (exception.StatusCode is null or not HttpStatusCode.Unauthorized) throw;

            throw new UnauthorizedAccessException("Invalid token");
        }

        Platform = new TelegramPlatform(flow, client, self);
    }

    protected override async Task WhenExecutingAsync(CancellationToken cancellationToken)
    {
        int offset = 0;

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var updates = await Platform!.BotApiClient.GetUpdatesAsync(new TelegramGetUpdatesRequest(offset), cancellationToken);

                if (updates.Count is 0) continue;

                offset = updates[^1].UpdateId + 1;

                _ = updates
                    .ToFrozenSequence()
                    .Parallelize()
                    .ForEachAsync((update, scopedCancellationToken) =>
                        ProcessUpdate(Platform!, update, scopedCancellationToken), cancellationToken)
                    .ContinueWith(task =>
                        HandleProcessUpdateFaults(cancellationToken, task), TaskContinuationOptions.ExecuteSynchronously);
            }
            catch (Exception exception)
            {
                _ = Flow.PublishUnobservedConnectionExceptionAsync(this, exception, cancellationToken);
            }
        }
    }

    protected override Task WhenDisposingAsync()
    {
        Platform?.Dispose();

        return Task.CompletedTask;
    }

    private async ValueTask ProcessUpdate(TelegramPlatform platform, TelegramUpdate update, CancellationToken cancellationToken)
    {
        if (update.Message is { } message)
        {
            var incomingMessage = IncomingMessageConverter.Convert(platform, message);

            if (incomingMessage is null) return;

            try
            {
                await Flow.PublishAsync(incomingMessage.ToMessageReceivedSignal(), cancellationToken);
            }
            catch (Exception exception)
            {
                _ = Flow.PublishUnobservedPublishingExceptionAsync(exception, cancellationToken);
            }
        }

        if (update.EditedMessage is { } editedMessage)
        {
            var incomingMessage = IncomingMessageConverter.Convert(platform, editedMessage);

            if (incomingMessage is null) return;

            try
            {
                await Flow.PublishAsync(incomingMessage.ToMessageEditedSignal(), cancellationToken);
            }
            catch (Exception exception)
            {
                _ = Flow.PublishUnobservedPublishingExceptionAsync(exception, cancellationToken);
            }
        }
    }

    private static TelegramBotProfile GetSelf(TelegramUser self)
    {
        return new TelegramBotProfile
        {
            Identifier = self.Id,
            FirstName = self.FirstName,
            LastName = self.LastName,
            NickName = self.Username
        };
    }

    private void HandleProcessUpdateFaults(CancellationToken cancellationToken, Task task)
    {
        if (task.IsFaulted is false || task.Exception is null) return;

        _ = Flow.PublishUnobservedConnectionExceptionAsync(this, task.Exception, cancellationToken);
    }
}
