using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
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
    public static TelegramIncomingMessage? Convert(TelegramPlatform platform, TelegramMessage message)
    {
        ArgumentNullException.ThrowIfNull(platform);
        ArgumentNullException.ThrowIfNull(message);

        if (message.Text.IsNullOrEmpty() is false)
        {
            return Convert(message.Text!, platform, message);
        }

        if (message.Sticker is { Type: TelegramStickerType.Regular } sticker)
        {
            return Convert(sticker, platform, message);
        }

        return null;
    }

    private static TelegramIncomingMessage? Convert(string text, TelegramPlatform platform, TelegramMessage message)
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

    private static TelegramIncomingMessage? Convert(TelegramSticker sticker, TelegramPlatform platform, TelegramMessage message)
    {
        if (TryGetProfilesContext(message, out var profilesContext) is false) return null;

        var client = platform.BotApiClient;

        var variants = new Sequence<TelegramMessageImageVariant>()
        {
            Convert(sticker, client)
        };

        if (sticker.Thumbnail is { } thumbnail)
        {
            variants.Add(Convert(thumbnail, client));
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

    private static TelegramMessageImageVariant Convert(TelegramPhotoSize photo, ITelegramClient client)
    {
        return new TelegramMessageImageVariant(async cancellation =>
        {
            var file = await client.GetFileAsync(new TelegramGetFileRequest(photo.FileId), cancellation);

            if (file.FilePath is null) throw new InvalidOperationException();

            return await client.DownloadAsync(file.FilePath, cancellation);
        })
        {
            Identifier = Identifier.FromValue(photo.FileUniqueId),
            Size = photo.FileSize ?? 0,
            Area = new Area(photo.Width, photo.Height)
        };
    }

    private static bool TryGetProfilesContext(TelegramMessage message, out ProfilesContext context)
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

    private static IReadOnlyCollection<IMessageTextStyle> GetStyles(IReadOnlyCollection<TelegramMessageEntity>? entities)
    {
        if (entities is null || entities.Count is 0) return [];

        return entities
            .Select<TelegramMessageEntity, IMessageTextStyle?>(entity => entity.Type switch
            {
                TelegramMessageEntities.Bold => new BoldTextStyle(entity.Offset, entity.Length),
                TelegramMessageEntities.Italic => new ItalicTextStyle(entity.Offset, entity.Length),
                TelegramMessageEntities.Underline => new UnderlineTextStyle(entity.Offset, entity.Length),
                TelegramMessageEntities.Strikethrough => new StrikethroughTextStyle(entity.Offset, entity.Length),
                TelegramMessageEntities.Code => new MonospaceTextStyle(entity.Offset, entity.Length),
                TelegramMessageEntities.BlockQuote => new QuotationTextStyle(entity.Offset, entity.Length),
                _ => null
            })
            .Where(style => style is not null)
            .Cast<IMessageTextStyle>()
            .ToFrozenSequence();
    }

    private static IProfile? GetEnvironment(TelegramMessage message)
    {
        if (message.Chat is not { } chat) return null;

        if (chat.Type is TelegramChatType.Private)
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

    private static IProfile? GetSender(TelegramMessage message)
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
