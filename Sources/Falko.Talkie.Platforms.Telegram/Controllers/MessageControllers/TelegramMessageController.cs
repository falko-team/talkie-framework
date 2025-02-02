using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Features;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;
using Talkie.Platforms;
using Talkie.Sequences;

namespace Talkie.Controllers.MessageControllers;

public sealed class TelegramMessageController(ISignalFlow flow,
    TelegramPlatform platform,
    GlobalMessageIdentifier identifier) : IMessageController
{
    public async Task<IIncomingMessage> PublishMessageAsync
    (
        IOutgoingMessage message,
        CancellationToken cancellationToken = default
    )
    {
        if (message.Content.IsEmpty)
        {
            throw new ArgumentException("Message content is required.");
        }

        if (identifier.EnvironmentIdentifier is not TelegramProfileIdentifier telegramEnvironmentProfileIdentifier)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (identifier.MessageIdentifier is not TelegramMessageIdentifier telegramMessageIdentifier)
        {
            throw new ArgumentException("Message id is required.");
        }

        TelegramIncomingMessage? sentMessage;

        var photoFactory = message
            .Attachments
            .OfType<TelegramMessageUrlImageAttachmentFactory>()
            .ToFrozenSequence();

        if (photoFactory.Count is 1)
        {
            var sendPhoto = new TelegramSendPhotoRequest
            (
                chatId: telegramEnvironmentProfileIdentifier.ProfileIdentifier,
                photo: photoFactory.First().Url,
                businessConnectionId: telegramMessageIdentifier.ConnectionIdentifier,
                caption: message.Content,
                captionEntities: GetEnitites(message.Content.Styles),
                disableNotification: message.Features.Any(t => t is SilenceMessageFeature),
                replyParameters: GetReplyParameters(message)
            );

            var sentRawMessage = await platform.BotApiClient.SendPhotoAsync
            (
                sendPhoto,
                cancellationToken
            );

            if (sentRawMessage.TryGetIncomingMessage(platform, out sentMessage) is false)
            {
                throw new InvalidOperationException("Failed to convert sent message.");
            }
        }
        else if (photoFactory.Count > 1)
        {
            var firstPhotoAttachment = photoFactory.First();

            var firstPhoto = new TelegramInputMediaPhoto
            (
                firstPhotoAttachment.Url,
                caption: message.Content,
                captionEntities: GetEnitites(message.Content.Styles)
            );

            var photos = photoFactory
                .Skip(1)
                .Select(photo => new TelegramInputMediaPhoto(photo.Url))
                .Append(firstPhoto)
                .ToFrozenSequence();

            var sendMessage = new TelegramSendMediaGroupRequest
            (
                chatId: telegramEnvironmentProfileIdentifier.ProfileIdentifier,
                media: photos,
                businessConnectionId: telegramMessageIdentifier.ConnectionIdentifier,
                disableNotification: message.Features.Any(t => t is SilenceMessageFeature),
                replyParameters: GetReplyParameters(message)
            );

            var sentRawMessage = await platform.BotApiClient.SendMediaGroupAsync
            (
                sendMessage,
                cancellationToken
            );

            if (sentRawMessage.TryGetIncomingMessage(platform, out sentMessage) is false)
            {
                throw new InvalidOperationException("Failed to convert sent message.");
            }
        }
        else
        {
            var sendMessage = new TelegramSendMessageRequest
            (
                telegramEnvironmentProfileIdentifier.ProfileIdentifier,
                message.Content,
                telegramMessageIdentifier.ConnectionIdentifier,
                GetEnitites(message.Content.Styles),
                message.Features.Any(t => t is SilenceMessageFeature),
                GetReplyParameters(message)
            );

            var sentRawMessage = await platform.BotApiClient.SendMessageAsync
            (
                sendMessage,
                cancellationToken
            );

            if (sentRawMessage.TryGetIncomingMessage(platform, out sentMessage) is false)
            {
                throw new InvalidOperationException("Failed to convert sent message.");
            }
        }

        flow.Publish(sentMessage.ToMessagePublishedSignal(), cancellationToken);

        return sentMessage;
    }

    public async Task<IIncomingMessage> ExchangeMessageAsync
    (
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessage message,
        CancellationToken cancellationToken = default
    )
    {
        if (message.Content.IsEmpty)
        {
            throw new ArgumentException("Message content is required.");
        }

        if (messageIdentifier.EnvironmentIdentifier is not TelegramProfileIdentifier telegramEnvironmentProfileIdentifier)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (messageIdentifier.MessageIdentifier is not TelegramMessageIdentifier telegramMessageIdentifier)
        {
            throw new ArgumentException("Message id is required.");
        }

        var editMessageText = new TelegramEditMessageTextRequest(
            message.Content.Text,
            entities: GetEnitites(message.Content.Styles),
            messageId: telegramMessageIdentifier.MessageIdentifier,
            businessConnectionId: telegramMessageIdentifier.ConnectionIdentifier,
            chatId: telegramEnvironmentProfileIdentifier.ProfileIdentifier);

        var sentMessage = await platform.BotApiClient.EditMessageTextAsync(editMessageText,
            cancellationToken: cancellationToken);

        if (sentMessage.TryGetIncomingMessage(platform, out var incomingMessage) is false)
        {
            throw new InvalidOperationException("Failed to convert sent message.");
        }

        flow.Publish(incomingMessage.ToMessageExchangedSignal(), cancellationToken);

        return incomingMessage;
    }

    public async Task UnpublishMessageAsync
    (
        GlobalMessageIdentifier messageIdentifier,
        CancellationToken cancellationToken = default
    )
    {
        if (messageIdentifier.EnvironmentIdentifier is not TelegramProfileIdentifier telegramEnvironmentProfileIdentifier)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (messageIdentifier.MessageIdentifier is not TelegramMessageIdentifier telegramMessageIdentifier)
        {
            throw new ArgumentException("Message id is required.");
        }

        if (telegramMessageIdentifier.ConnectionIdentifier is not null)
        {
            throw new NotSupportedException("Business connection id is not supported.");
        }

        var deleteMessage = new TelegramDeleteMessageRequest
        (
            telegramMessageIdentifier.MessageIdentifier,
            telegramEnvironmentProfileIdentifier.ProfileIdentifier
        );

        if (await platform.BotApiClient.DeleteMessageAsync(deleteMessage, cancellationToken) is false)
        {
            throw new InvalidOperationException("Message was not found.");
        }
    }

    private static IReadOnlyCollection<TelegramMessageEntity>? GetEnitites(IReadOnlyCollection<IMessageTextStyle> styles)
    {
        if (styles.Count is 0) return null;

        return styles
            .Select(style => style switch
            {
                BoldTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Bold, style.Offset, style.Length),
                ItalicTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Italic, style.Offset, style.Length),
                LinkTextStyle linkTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Link, style.Offset, style.Length, linkTextStyle.Link),
                UnderlineTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Underline, style.Offset, style.Length),
                StrikethroughTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Strikethrough, style.Offset, style.Length),
                MonospaceTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Code, style.Offset, style.Length),
                QuotationTextStyle => new TelegramMessageEntity(TelegramMessageEntities.BlockQuote, style.Offset, style.Length),
                _ => null
            })
            .Where(entity => entity is not null)
            .Cast<TelegramMessageEntity>()
            .ToFrozenSequence();
    }

    private static TelegramReplyParameters? GetReplyParameters(IOutgoingMessage outgoingMessage)
    {
        if (outgoingMessage.Reply is null) return null;

        if (outgoingMessage.Reply.MessageIdentifier is not TelegramMessageIdentifier replyMessageIdentifier)
        {
            throw new ArgumentException("Reply message identifier is required.");
        }

        if (replyMessageIdentifier.ConnectionIdentifier is not null)
        {
            return new TelegramReplyParameters(replyMessageIdentifier.MessageIdentifier);
        }

        if (outgoingMessage.Reply.EnvironmentIdentifier is not TelegramProfileIdentifier replyEnvironmentIdentifier)
        {
            throw new ArgumentException("Reply message environment identifier is required.");
        }

        return new TelegramReplyParameters(replyMessageIdentifier.MessageIdentifier, replyEnvironmentIdentifier.ProfileIdentifier);
    }
}
