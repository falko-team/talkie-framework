using Talkie.Bridges.Telegram.Models;
using Talkie.Models.Entries;
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
            Id = message.MessageId,
            Content = text,
            Platform = platform,
            Entry = new TelegramEntry
            {
                Sender = sender,
                Sent = message.Date ?? received,
                Receiver = platform.Self,
                Received = received,
                Environment = receiver
            }
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
}
