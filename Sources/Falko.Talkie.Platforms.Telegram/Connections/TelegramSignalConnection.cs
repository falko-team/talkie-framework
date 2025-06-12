using System.Net;
using Falko.Talkie.Bridges.Telegram.Clients;
using Falko.Talkie.Bridges.Telegram.Configurations;
using Falko.Talkie.Bridges.Telegram.Exceptions;
using Falko.Talkie.Bridges.Telegram.Policies;
using Falko.Talkie.Bridges.Telegram.Requests;
using Falko.Talkie.Converters;
using Falko.Talkie.Flows;
using Falko.Talkie.Models.Profiles;
using Falko.Talkie.Platforms;

namespace Falko.Talkie.Connections;

public sealed class TelegramSignalConnection : ModernSignalConnection, IWithPlatformSignalConnection
{
    private readonly ISignalFlow _flow;

    private readonly TelegramConfiguration _configuration;

    private readonly CancellationTokenSource _executingCts = new();

    private readonly TelegramTooManyRequestLocalRetryPolicy _retryPolicy;

    private TelegramPlatform _platform = null!;

    private Task _executingTask = null!;

    internal TelegramSignalConnection(ISignalFlow flow, TelegramConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(flow);
        configuration.ThrowIfInvalid();

        _flow = flow;
        _configuration = configuration;

        _retryPolicy = new TelegramTooManyRequestLocalRetryPolicy
        (
            minimumDelay: configuration.ServerConfiguration.DefaultRetryDelay
        );
    }

    public IPlatform? Platform => IsInitialized ? _platform : null;

    protected override async ValueTask InitializeCoreAsync(CancellationToken cancellationToken)
    {
        var client = new TelegramClient(_configuration);

        var self = await GetSelfAsync(client, cancellationToken);

        _platform = new TelegramPlatform(_flow, client, self, _configuration.ServerConfiguration.DefaultRetryDelay);

        _executingTask = Task
            .Factory
            .StartNew
            (
                function: () => ExecuteAsync(_executingCts.Token),
                cancellationToken: _executingCts.Token,
                creationOptions: TaskCreationOptions.LongRunning,
                scheduler: TaskScheduler.Default
            );
    }

    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var publisher = new TelegramUpdatePublisher(_flow, _platform);
        var offset = 0;

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var updates = await _platform
                    .Client
                    .GetUpdatesAsync(new TelegramGetUpdatesRequest(offset), _retryPolicy, cancellationToken);

                publisher.Handle(updates, cancellationToken);

                if (updates.Count is 0) continue;

                offset = updates[^1].UpdateId + 1;
            }
            catch (Exception exception)
            {
                _ = _flow.PublishUnobservedConnectionExceptionAsync(this, exception, cancellationToken);
            }
        }
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _executingCts.CancelAsync();
        _executingCts.Dispose();

        await _executingTask;
        _executingTask.Dispose();

        _platform.Dispose();
    }

    private async Task<IBotProfile> GetSelfAsync(TelegramClient client, CancellationToken cancellationToken)
    {
        try
        {
            return (await client.GetMeAsync(cancellationToken: cancellationToken)).ToBotProfile();
        }
        catch (TelegramException exception)
        {
            if (exception.StatusCode is null or not HttpStatusCode.Unauthorized) throw;

            throw new UnauthorizedAccessException("Invalid telegram bot token", exception);
        }
    }
}
