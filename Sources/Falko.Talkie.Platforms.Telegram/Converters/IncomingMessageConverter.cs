using Talkie.Bridges.Telegram.Models;
using Talkie.Common;
using Talkie.Localizations;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments;
using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Sequences;

namespace Talkie.Converters;

internal static class IncomingMessageConverter
{
    public static TelegramIncomingMessage? Convert(TelegramPlatform platform, Message message)
    {
        ArgumentNullException.ThrowIfNull(platform);
        ArgumentNullException.ThrowIfNull(message);

        if (message.Text.IsNullOrEmpty() is false)
        {
            return Convert(message.Text!, platform, message);
        }

        if (message.Sticker is { Type: StickerType.Regular } sticker)
        {
            return Convert(sticker, platform, message);
        }

        return null;
    }

    private static TelegramIncomingMessage? Convert(string text, TelegramPlatform platform, Message message)
    {
        if (TryGetProfilesContext(message, out var profilesContext) is false) return null;

        return new TelegramIncomingMessage
        {
            Identifier = message.MessageId,
            Content = new MessageContent(text, GetStyles(message.Entities)),
            Platform = platform,
            Reply = message.ReplyToMessage is { } reply
                ? Convert(platform, reply)
                : null,
            PublisherProfile = profilesContext.PublisherProfile,
            PublishedDate = message.Date ?? DateTime.UtcNow,
            ReceiverProfile = platform.BotProfile,
            ReceivedDate = DateTime.UtcNow,
            EnvironmentProfile = profilesContext.EnvironmentProfile
        };
    }

    private static TelegramIncomingMessage? Convert(Sticker sticker, TelegramPlatform platform, Message message)
    {
        if (TryGetProfilesContext(message, out var profilesContext) is false) return null;

        var variants = new Sequence<TelegramMessageImageVariant>()
        {
            Convert(sticker)
        };

        if (sticker.Thumbnail is { } thumbnail)
        {
            variants.Add(Convert(thumbnail));
        }

        var telegramSticker = new TelegramMessageStickerAttachment
        {
            Identifier = Identifier.FromValue(sticker.FileUniqueId),
            Variants = variants.ToFrozenSequence()
        };

        return new TelegramIncomingMessage
        {
            Identifier = message.MessageId,
            Platform = platform,
            Reply = message.ReplyToMessage is { } reply
                ? Convert(platform, reply)
                : null,
            Content = MessageContent.Empty,
            Attachments = new FrozenSequence<IMessageAttachment>([telegramSticker]),
            PublisherProfile = profilesContext.PublisherProfile,
            PublishedDate = message.Date ?? DateTime.UtcNow,
            ReceiverProfile = platform.BotProfile,
            ReceivedDate = DateTime.UtcNow,
            EnvironmentProfile = profilesContext.EnvironmentProfile
        };
    }

    private static TelegramMessageImageVariant Convert(PhotoSize photo, string? name = null, string? type = null)
    {
        return new TelegramMessageImageVariant
        {
            Identifier = Identifier.FromValue(photo.FileUniqueId),
            Size = photo.FileSize ?? 0,
            Area = new Area(photo.Width, photo.Height)
        };
    }

    private static bool TryGetProfilesContext(Message message, out ProfilesContext context)
    {
        var sender = GetSender(message);

        if (sender is null)
        {
            context = default;
            return false;
        }

        var environment = GetEnvironment(message);

        if (environment is null)
        {
            context = default;
            return false;
        }

        context = new ProfilesContext(sender, environment);
        return true;
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
                MessageEntities.BlockQuote => new QuotationTextStyle(entity.Offset, entity.Length),
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

    private readonly record struct ProfilesContext(IProfile PublisherProfile, IProfile EnvironmentProfile);
}
