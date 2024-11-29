using System.Net;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Configurations;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Connections;

public sealed class TelegramSignalConnection : ModernSignalConnection, IWithPlatformSignalConnection
{
    private readonly ISignalFlow _flow;

    private readonly TelegramConfiguration _configuration;

    private readonly CancellationTokenSource _executingCts = new();

    private TelegramPlatform _platform = null!;
    private Task _executingTask = null!;

    internal TelegramSignalConnection(ISignalFlow flow, TelegramConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(flow);
        configuration.ThrowIfInvalid();

        _flow = flow;
        _configuration = configuration;
    }

    public IPlatform? Platform => IsInitialized ? _platform : null;

    protected override async ValueTask InitializeCoreAsync(CancellationToken cancellationToken)
    {
        var client = new TelegramClient(_configuration);

        var self = await GetSelfAsync(client, cancellationToken);

        _platform = new TelegramPlatform(_flow, client, self);

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
        var publisher = new TelegramUpdatePublisher(_flow, _platform, this);
        var offset = 0;

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                var updates = await _platform
                    .BotApiClient
                    .GetUpdatesAsync(new TelegramGetUpdatesRequest(offset), cancellationToken);

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

    private static async Task<IBotProfile> GetSelfAsync(TelegramClient client, CancellationToken cancellationToken)
    {
        try
        {
            return (await client.GetMeAsync(cancellationToken)).ToBotProfile();
        }
        catch (TelegramException exception)
        {
            if (exception.StatusCode is null or not HttpStatusCode.Unauthorized) throw;

            throw new UnauthorizedAccessException("Invalid telegram bot token", exception);
        }
    }
}
