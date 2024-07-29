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

        var sender = GetSender(message);

        var environment = GetEnvironment(message);

        if (sender is null || environment is null) return null;

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
            EnvironmentProfile = environment
        };
    }

    private static IProfile? GetEnvironment(Message message)
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

    private static IProfile? GetSender(Message message)
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
                    LastName = sender.LastName,
                    Language = sender.LanguageCode
                };
            }

            return new TelegramUserProfile
            {
                Identifier = sender.Id,
                NickName = sender.Username,
                FirstName = sender.FirstName,
                LastName = sender.LastName,
                Language = sender.LanguageCode
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
