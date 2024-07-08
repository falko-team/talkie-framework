using Talkie.Bridges.Telegram.Models;
using Talkie.Models.Messages;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Validations;
using Message = Talkie.Bridges.Telegram.Models.Message;

namespace Talkie.Converters;

internal static class IncomingMessageConverter
{
    public static TelegramIncomingMessage? Convert(TelegramPlatform platform, Message message)
    {
        message.ThrowIf().Null();

        if (message.Text is not { } text) return null;

        var sender = GetMessageSender(message);

        var receiver = GetReceiver(message);

        if (sender is null || receiver is null) return null;

        var received = DateTime.UtcNow;

        return new TelegramIncomingMessage
        {
            Identifier = message.MessageId,
            Text = text,
            Platform = platform,
            Reply = message.ReplyToMessage is { } reply
                ? Convert(platform, reply)
                : null,
            SenderProfile = sender,
            Sent = message.Date ?? received,
            ReceiverProfile = platform.BotProfile,
            Received = received,
            EnvironmentProfile = receiver
        };
    }

    private static IProfile? GetReceiver(Message message)
    {
        if (message.Chat is not { } chat) return null;

        if (chat.Type is ChatType.Private)
        {
            return new TelegramUserProfile
            {
                Identifier = chat.Id,
                NickName = chat.Username,
                FirstName = chat.FirstName,
                LastName = chat.LastName
            };
        }

        return new TelegramChatProfile
        {
            Identifier = chat.Id,
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
                    Identifier = sender.Id,
                    NickName = sender.Username,
                    FirstName = sender.FirstName,
                    LastName = sender.LastName
                };
            }

            return new TelegramUserProfile
            {
                Identifier = sender.Id,
                NickName = sender.Username,
                FirstName = sender.FirstName,
                LastName = sender.LastName
            };
        }

        if (message.Chat is { } chat)
        {
            return new TelegramChatProfile
            {
                Identifier = chat.Id,
                Title = chat.Title,
                NickName = chat.Username
            };
        }

        return null;
    }
}
