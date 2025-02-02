using System.Diagnostics.CodeAnalysis;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Connections;
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

internal static class TelegramConverters
{
    public static bool TryGetIncomingMessage
    (
        this IReadOnlyCollection<TelegramMessage> messages,
        TelegramPlatform platform,
        [NotNullWhen(true)] out TelegramIncomingMessage? incomingMessage
    )
    {
        if (messages.Count is 0)
        {
            incomingMessage = null;
            return false;
        }

        var firstMessage = messages.Sequencing().First();

        if (firstMessage.TryGetPublisherProfile(out var publisherProfile) is false)
        {
            incomingMessage = null;
            return false;
        }

        if (firstMessage.TryGetEnvironmentProfile(out var environmentProfile) is false)
        {
            incomingMessage = null;
            return false;
        }

        var context = new TelegramMessageBuildContext
        (
            messageIdentifier: firstMessage.MessageId,
            connectionIdentifier: firstMessage.BusinessConnectionId,
            platform: platform,
            environmentProfile: environmentProfile,
            publisherProfile: publisherProfile,
            publishedDate: firstMessage.Date,
            reply: firstMessage.ReplyToMessage
        );

        var attachments = new Sequence<IMessageAttachment>();

        foreach (var telegramMessage in messages)
        {
            if (telegramMessage.TryGetImageAttachment(platform.BotApiClient, out var messageImageAttachment) is false)
            {
                continue;
            }

            attachments.Add(messageImageAttachment);
        }

        if (attachments.Count is 0)
        {
            incomingMessage = null;
            return false;
        }

        incomingMessage = context.ToIncomingMessage(attachments: attachments.ToFrozenSequence());
        return true;
    }

    public static bool TryGetIncomingMessage
    (
        this TelegramMessage message,
        TelegramPlatform platform,
        [NotNullWhen(true)] out TelegramIncomingMessage? incomingMessage
    )
    {
        if (message.TryGetPublisherProfile(out var publisherProfile) is false)
        {
            incomingMessage = null;
            return false;
        }

        if (message.TryGetEnvironmentProfile(out var environmentProfile) is false)
        {
            incomingMessage = null;
            return false;
        }

        var context = new TelegramMessageBuildContext
        (
            messageIdentifier: message.MessageId,
            connectionIdentifier: message.BusinessConnectionId,
            platform: platform,
            environmentProfile: environmentProfile,
            publisherProfile: publisherProfile,
            publishedDate: message.Date,
            reply: message.ReplyToMessage
        );

        if (message.TryGetMessageTextContent(out var messageContent))
        {
            incomingMessage = context.ToIncomingMessage(messageContent);

            return true;
        }

        if (message.TryGetImageAttachment(platform.BotApiClient, out var imageAttachment))
        {
            var attachments = new FrozenSequence<IMessageAttachment>(imageAttachment);

            incomingMessage = context.ToIncomingMessage(attachments: attachments);

            return true;
        }

        if (message.TryGetStickerAttachment(platform.BotApiClient, out var stickerAttachment))
        {
            var attachments = new FrozenSequence<IMessageAttachment>(stickerAttachment);

            incomingMessage = context.ToIncomingMessage(attachments: attachments);

            return true;
        }

        incomingMessage = null;
        return false;
    }

    public static TelegramIncomingMessage ToIncomingMessage
    (
        this TelegramMessageBuildContext context,
        MessageContent? content = null,
        IReadOnlySequence<IMessageAttachment>? attachments = null
    )
    {
        var environmentProfile = context.EnvironmentProfile;
        var publisherProfile = context.PublisherProfile;
        var receiverProfile = context.ReceiverProfile;

        return new TelegramIncomingMessage
        {
            Identifier = new TelegramMessageIdentifier(context.MessageIdentifier, context.ConnectionIdentifier),
            Platform = context.Platform,
            Reply = context.Reply is { } replyMessage
                && replyMessage.TryGetIncomingMessage(context.Platform, out var reply)
                    ? reply
                    : null,
            EnvironmentProfile = environmentProfile,
            PublisherProfile = publisherProfile,
            ReceiverProfile = receiverProfile,
            PublishedDate = context.PublishedDate,
            ReceivedDate = context.ReceivedDate,
            Content = content ?? MessageContent.Empty,
            Attachments = attachments ?? FrozenSequence<IMessageAttachment>.Empty
        };
    }

    public static bool TryGetImageAttachment
    (
        this TelegramMessage message,
        ITelegramClient client,
        [NotNullWhen(true)] out IMessageImageAttachment? attachment
    )
    {
        if (message.Photo is not { } photo || photo.Count is 0)
        {
            attachment = null;
            return false;
        }

        var variants = new Sequence<IMessageImageVariant>();

        foreach (var size in photo)
        {
            variants.Add(size.ToImageVariant(client));
        }

        var identifier = message.MediaGroupId is not null
            ? new TelegramMessageFileAttachmentGroupIdentifier(message.MediaGroupId)
            : TelegramMessageFileAttachmentGroupIdentifier.Empty;

        if (message.TryGetMessageCaptionContent(out var caption))
        {
            attachment = new MessageImageAttachment
            {
                Identifier = identifier,
                Content = caption,
                Variants = variants.ToFrozenSequence()
            };
        }
        else
        {
            attachment = new MessageImageAttachment
            {
                Identifier = identifier,
                Variants = variants.ToFrozenSequence()
            };
        }

        return true;
    }

    public static bool TryGetStickerAttachment
    (
        this TelegramMessage message,
        ITelegramClient client,
        [NotNullWhen(true)] out IMessageStickerAttachment? attachment
    )
    {
        if (message.Sticker is not { } sticker)
        {
            attachment = null;
            return false;
        }

        var variants = new Sequence<IMessageImageVariant>
        {
            sticker.ToImageVariant(client)
        };

        if (sticker.Thumbnail is { } thumbnail)
        {
            variants.Add(thumbnail.ToImageVariant(client));
        }

        attachment = new MessageStickerAttachment
        {
            Identifier = new TelegramMessageFileAttachmentIdentifier(sticker.FileUniqueId, sticker.FileId),
            Variants = variants.ToFrozenSequence()
        };

        return true;
    }

    public static bool TryGetMessageTextContent
    (
        this TelegramMessage message,
        out MessageContent content
    )
    {
        if (message.Text is not { } text)
        {
            content = MessageContent.Empty;
            return false;
        }

        content = new MessageContent(text, message.Entities.GetMessageTextStyles());
        return true;
    }

    public static bool TryGetMessageCaptionContent
    (
        this TelegramMessage message,
        out MessageContent content
    )
    {
        if (message.Caption is not { } text)
        {
            content = MessageContent.Empty;
            return false;
        }

        content = new MessageContent(text, message.CaptionEntities.GetMessageTextStyles());
        return true;
    }

    public static IReadOnlyCollection<IMessageTextStyle> GetMessageTextStyles
    (
        this IReadOnlyCollection<TelegramMessageEntity>? entities
    )
    {
        if (entities is null || entities.Count is 0)
        {
            return FrozenSequence<IMessageTextStyle>.Empty;
        }

        var styles = new Sequence<IMessageTextStyle>();

        foreach (var entity in entities)
        {
            if (entity.TryGetMessageTextStyle(out var style) is false) continue;

            styles.Add(style);
        }

        // The count will be validated in the 'ToFrozenSequence()' method,
        // so no additional checks are needed before.
        return styles.ToFrozenSequence();
    }

    public static bool TryGetPublisherProfile
    (
        this TelegramMessage message,
        [MaybeNullWhen(false)] out IProfile profile
    )
    {
        if (message.From is { } sender)
        {
            var language = Language.Unknown;

            if (sender.LanguageCode is not null)
            {
                Languages.TryGetFromLanguageCodeName(sender.LanguageCode, out language);
            }

            profile = sender.IsBot
                ? sender.ToBotProfile(language)
                : sender.ToUserProfile(language);

            return true;
        }

        if (message.SenderChat is { Type: not TelegramChatType.Private } chat)
        {
            profile = chat.ToChatProfile();
            return true;
        }

        profile = null;
        return false;
    }

    public static bool TryGetEnvironmentProfile
    (
        this TelegramMessage message,
        [MaybeNullWhen(false)] out IProfile profile
    )
    {
        if (message.Chat is not { } chat)
        {
            profile = null;
            return false;
        }

        if (chat.Type is TelegramChatType.Private)
        {
            profile = chat.ToUserProfile();
            return true;
        }

        profile = chat.ToChatProfile();
        return true;
    }

    public static bool TryGetMessageTextStyle
    (
        this TelegramMessageEntity entity,
        [MaybeNullWhen(false)] out IMessageTextStyle style
    )
    {
        style = entity.Type switch
        {
            TelegramMessageEntities.Bold => new BoldTextStyle(entity.Offset, entity.Length),
            TelegramMessageEntities.Italic => new ItalicTextStyle(entity.Offset, entity.Length),
            TelegramMessageEntities.Link => entity.Url is { } url
                ? new LinkTextStyle(entity.Offset, entity.Length, url)
                : null,
            TelegramMessageEntities.Underline => new UnderlineTextStyle(entity.Offset, entity.Length),
            TelegramMessageEntities.Strikethrough => new StrikethroughTextStyle(entity.Offset, entity.Length),
            TelegramMessageEntities.BlockQuote => new QuotationTextStyle(entity.Offset, entity.Length),
            TelegramMessageEntities.Code => new MonospaceTextStyle(entity.Offset, entity.Length),
            _ => null
        };

        return style is not null;
    }

    public static IMessageImageVariant ToImageVariant(this TelegramPhotoSize photo, ITelegramClient client)
    {
        // TODO: Refactor 'streamFactory' to use controllers.
        return new MessageImageVariant(async cancellation =>
        {
            var file = await client.GetFileAsync(new TelegramGetFileRequest(photo.FileId), cancellation);

            if (file.FilePath is null) throw new InvalidOperationException();

            return await client.DownloadAsync(file.FilePath, cancellation);
        })
        {
            Identifier = new TelegramMessageFileAttachmentIdentifier(photo.FileUniqueId, photo.FileId),
            Size = photo.FileSize ?? 0,
            Area = new Area(photo.Width, photo.Height)
        };
    }

    public static IBotProfile ToBotProfile(this TelegramUser user, Language language = Language.Unknown)
    {
        return new BotProfile
        {
            Identifier = new TelegramProfileIdentifier(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            NickName = user.Username,
            Language = language
        };
    }

    public static IChatProfile ToChatProfile(this TelegramChat user)
    {
        return new ChatProfile
        {
            Identifier = new TelegramProfileIdentifier(user.Id),
            Title = user.Title,
            NickName = user.Username
        };
    }

    public static IUserProfile ToUserProfile(this TelegramUser user, Language language = Language.Unknown)
    {
        return new UserProfile
        {
            Identifier = new TelegramProfileIdentifier(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            NickName = user.Username,
            Language = language
        };
    }

    public static IUserProfile ToUserProfile(this TelegramChat user, Language language = Language.Unknown)
    {
        return new UserProfile
        {
            Identifier = new TelegramProfileIdentifier(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            NickName = user.Username,
            Language = language
        };
    }
}
