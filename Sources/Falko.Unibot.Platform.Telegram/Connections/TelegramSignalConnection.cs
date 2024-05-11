using Falko.Unibot.Bridges.Telegram.Clients;
using Falko.Unibot.Bridges.Telegram.Models;
using Falko.Unibot.Flows;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Platforms;
using Falko.Unibot.Signals;

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

    private void ProcessUpdate(IPlatform platform,
        Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;

        if (message.Text is not { } text) return;

        var sender = GetMessageSender(message);

        var receiver = GetReceiver(message);

        if (sender is null || receiver is null) return;

        var incomingMessage = new TelegramIncomingMessage
        {
            Id = message.MessageId,
            Content = text,
            Sender = sender,
            Receiver = receiver,
            Platform = platform,
            Sent = message.Date ?? DateTime.MinValue,
            Received = DateTime.UtcNow
        };

        _ = flow.PublishAsync(new IncomingMessageSignal(incomingMessage), cancellationToken);
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

    private static IProfile? GetReceiver(Message message)
    {
        if (message.Chat is not { } chat) return null;

        if (chat.Type is ChatType.Private)
        {
            return new TelegramUserProfile
            {
                Id = chat.Id,
                NickName = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };
        }

        return new TelegramChatProfile
        {
            Id = chat.Id,
            Title = chat.Title,
        };
    }

    private static IProfile? GetMessageSender(Message message)
    {
        if (message.From is { } sender)
        {
            if (sender.IsBot)
            {
                return new TelegramBotProfile
                {
                    Id = sender.Id,
                    NickName = sender.Username,
                    FirstName = sender.FirstName,
                    LastName = sender.LastName
                };
            }

            return new TelegramUserProfile
            {
                Id = sender.Id,
                NickName = sender.Username,
                FirstName = sender.FirstName,
                LastName = sender.LastName
            };
        }

        if (message.Chat is { } chat)
        {
            return new TelegramChatProfile
            {
                Id = chat.Id,
                Title = chat.Title,
            };
        }

        return null;
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
