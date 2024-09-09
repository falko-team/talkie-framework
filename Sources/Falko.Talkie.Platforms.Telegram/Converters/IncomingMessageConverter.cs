using Talkie.Bridges.Telegram.Models;
using Talkie.Collections;
using Talkie.Localizations;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Validations;

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
            Content = new MessageContent(text, GetStyles(message.Entities)),
            Platform = platform,
            Reply = message.ReplyToMessage is { } reply
                ? Convert(platform, reply)
                : null,
            PublisherProfile = sender,
            PublishedDate = message.Date ?? received,
            ReceiverProfile = platform.BotProfile,
            ReceivedDate = received,
            EnvironmentProfile = environment
        };
    }

    private static IReadOnlyCollection<IMessageTextStyle> GetStyles(IReadOnlyCollection<MessageEntity>? entities)
    {
        if (entities is null || entities.Count is 0) return [];

        return entities
            .Select<MessageEntity, IMessageTextStyle?>(entity => entity.Type switch
            {
                MessageEntities.Bold => new BoldTextStyle(entity.Offset, entity.Length),
                MessageEntities.Italic => new ItalicTextStyle(entity.Offset, entity.Length),
                MessageEntities.Underline => new UnderlineTextStyle(entity.Offset, entity.Length),
                MessageEntities.Strikethrough => new StrikethroughTextStyle(entity.Offset, entity.Length),
                MessageEntities.Code => new MonospaceTextStyle(entity.Offset, entity.Length),
                _ => null
            })
            .Where(style => style is not null)
            .Cast<IMessageTextStyle>()
            .ToFrozenSequence();
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
            var language = Language.Unknown;

            if (sender.LanguageCode is not null)
            {
                if (LanguageCodes.TryGetLanguageCode(sender.LanguageCode, out var languageCode))
                {
                    languageCode.TryGetLanguage(out language);
                }
            }

            if (sender.IsBot)
            {
                return new TelegramBotProfile
                {
                    Identifier = sender.Id,
                    NickName = sender.Username,
                    FirstName = sender.FirstName,
                    LastName = sender.LastName,
                    Language = language
                };
            }

            return new TelegramUserProfile
            {
                Identifier = sender.Id,
                NickName = sender.Username,
                FirstName = sender.FirstName,
                LastName = sender.LastName,
                Language = language
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
