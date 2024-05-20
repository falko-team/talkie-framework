using Falko.Unibot.Bridges.Telegram.Clients;
using Falko.Unibot.Bridges.Telegram.Models;
using Falko.Unibot.Converters;
using Falko.Unibot.Flows;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Platforms;
using Falko.Unibot.Signals;
using Message = Falko.Unibot.Bridges.Telegram.Models.Message;

namespace Falko.Unibot.Connections;

public sealed class TelegramSignalConnection(ISignalFlow flow, string token) : ISignalConnection
{
    private CancellationTokenSource? _globalCancellationTokenSource;

    private Task? _processUpdatesTask;

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        _globalCancellationTokenSource = new();

        var client = new TelegramBotApiClient(token);

        var self = GetSelf(await client.GetMeAsync(cancellationToken));

        var platform = new TelegramPlatform(client, self);

        _processUpdatesTask = client.ProcessUpdatesAsync((update, scopedCancellationToken) =>
                ProcessUpdate(platform, update, scopedCancellationToken),
            _globalCancellationTokenSource.Token);
    }

    private void ProcessUpdate(TelegramPlatform platform,
        Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;

        var incomingMessage = IncomingMessageConverter.Convert(platform, message);

        if (incomingMessage is null) return;

        _ = flow.PublishAsync(new TelegramIncomingMessageSignal(incomingMessage), cancellationToken);
    }

    private static IBotProfile GetSelf(User self)
    {
        return new TelegramBotProfile
        {
            Id = self.Id,
            FirstName = self.FirstName,
            LastName = self.LastName,
            NickName = self.Username
        };
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
