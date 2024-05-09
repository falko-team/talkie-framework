using Falko.Unibot.Flows;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Platforms;
using Falko.Unibot.Signals;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Falko.Unibot.Connections;

public sealed class TelegramSignalConnection(ISignalFlow flow, string token) : ISignalConnection
{
    private CancellationTokenSource? _cts;

    private IPlatform? _platform;

    private DateTime _startedDate;

    private TelegramBotClient? _client;

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_cts is not null)
        {
            throw new InvalidOperationException("The connection is already initialized.");
        }

        _startedDate = DateTime.UtcNow;

        _cts = new CancellationTokenSource();

        _client = new TelegramBotClient(token);

        using var scopedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, cancellationToken);

        var me = CreateBotProfile(await _client.GetMeAsync(scopedCts.Token));

        _platform = new TelegramPlatform(_client, me);

        var options = new ReceiverOptions
        {
            AllowedUpdates = [ UpdateType.Message ]
        };

        _client.StartReceiving(
            updateHandler: UpdateHandler,
            pollingErrorHandler: PollingErrorHandler,
            receiverOptions: options,
            cancellationToken: _cts.Token
        );
    }

    private async Task PollingErrorHandler(ITelegramBotClient client, Exception ex, CancellationToken cancellationToken)
    {
        await flow.PublishAsync(new UnhandledExceptionSignal(this, ex), cancellationToken);
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;

        if (message.Date < _startedDate) return;

        if (message.Text is not { } messageText) return;

        var incomingMessage = new TelegramIncomingMessage
        {
            Id = message.MessageId,
            Receiver = Receiver(message),
            Sender = GetProfile(message.From!),
            Sent = message.Date,
            Received = DateTime.UtcNow,
            Content = messageText,
            Platform = _platform!
        };

        await flow.PublishAsync(new IncomingMessageSignal(incomingMessage), cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        _cts?.Cancel();

        return ValueTask.CompletedTask;
    }

    private IProfile Receiver(Message message)
    {
        var chat = _client!.GetChatAsync(message.Chat).Result;

        if (chat is { Type: ChatType.Private } chat1)
        {
            return GetProfile(chat1);
        }

        if (chat is { Type: ChatType.Group or ChatType.Supergroup } chat2)
        {
            return GetChatProfile(chat2);
        }

        throw new NotSupportedException("Unsupported chat type.");
    }

    private static IChatProfile GetChatProfile(Chat chat)
    {
        return new TelegramChatProfile
        {
            Id = chat.Id,
            Title = chat.Title,
            Description = chat.Description,
        };
    }

    private static IProfile GetProfile(Chat chat)
    {
        return new TelegramUserProfile
        {
            Id = chat.Id,
            NickName = chat.Username,
            FirstName = chat.FirstName,
            LastName = chat.LastName,
            Description = chat.Bio
        };
    }

    private static IProfile GetProfile(User user)
    {
        return user.IsBot ? CreateBotProfile(user) : CreateUserProfile(user);
    }

    private static IBotProfile CreateBotProfile(User me)
    {
        return new TelegramBotProfile
        {
            Id = me.Id,
            NickName = me.Username,
            FirstName = me.FirstName,
            LastName = me.LastName
        };
    }

    private static IUserProfile CreateUserProfile(User me)
    {
        return new TelegramUserProfile
        {
            Id = me.Id,
            NickName = me.Username,
            FirstName = me.FirstName,
            LastName = me.LastName
        };
    }
}
