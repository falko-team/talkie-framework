using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Bridges.Telegram.Requests;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;
using Talkie.Platforms;
using Talkie.Sequences;

namespace Talkie.Controllers.MessageControllers;

public sealed class TelegramMessageController(ISignalFlow flow,
    TelegramPlatform platform,
    Identifier environmentProfileIdentifier) : IMessageController
{
    public async Task<IIncomingMessage> PublishMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        if (message.Content.IsEmpty)
        {
            throw new ArgumentException("Message content is required.");
        }

        if (environmentProfileIdentifier.TryGetValue(out long receiverId) is false)
        {
            throw new ArgumentException("Environment id is required.");
        }

        var sendMessage = new TelegramSendMessageRequest(
            receiverId,
            message.Content,
            GetEnitites(message.Content.Styles),
            features.PublishSilently,
            GetReplyParameters(message));

        var sentMessage = await platform.BotApiClient.SendMessageAsync(sendMessage,
            cancellationToken: cancellationToken);

        if (sentMessage.TryGetIncomingMessage(platform, out var incomingMessage) is false)
        {
            throw new InvalidOperationException("Failed to convert sent message.");
        }

        flow.Publish(incomingMessage.ToMessagePublishedSignal(), cancellationToken);

        return incomingMessage;
    }

    public async Task<IIncomingMessage> ExchangeMessageAsync(GlobalMessageIdentifier messageIdentifier, IOutgoingMessage message,
        CancellationToken cancellationToken = default)
    {
        if (message.Content.IsEmpty)
        {
            throw new ArgumentException("Message content is required.");
        }

        if (messageIdentifier.EnvironmentIdentifier.TryGetValue(out long environmentTelegramId) is false)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (messageIdentifier.MessageIdentifier.TryGetValue(out long messageTelegramId) is false)
        {
            throw new ArgumentException("Message id is required.");
        }

        var editMessageText = new TelegramEditMessageTextRequest(
            message.Content.Text,
            entities: GetEnitites(message.Content.Styles),
            messageId: messageTelegramId,
            chatId: environmentTelegramId);

        var sentMessage = await platform.BotApiClient.EditMessageTextAsync(editMessageText,
            cancellationToken: cancellationToken);

        if (sentMessage.TryGetIncomingMessage(platform, out var incomingMessage) is false)
        {
            throw new InvalidOperationException("Failed to convert sent message.");
        }

        flow.Publish(incomingMessage.ToMessageExchangedSignal(), cancellationToken);

        return incomingMessage;
    }

    public async Task UnpublishMessageAsync(GlobalMessageIdentifier messageIdentifier, CancellationToken cancellationToken = default)
    {
        if (messageIdentifier.EnvironmentIdentifier.TryGetValue(out long environmentTelegramId) is false)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (messageIdentifier.MessageIdentifier.TryGetValue(out long messageTelegramId) is false)
        {
            throw new ArgumentException("Message id is required.");
        }

        var deleteMessage = new TelegramDeleteMessageRequest(messageTelegramId, environmentTelegramId);

        if (await platform.BotApiClient.DeleteMessageAsync(deleteMessage, cancellationToken) is false)
        {
            throw new InvalidOperationException("Message was not found.");
        }
    }

    private IReadOnlyCollection<TelegramMessageEntity>? GetEnitites(IReadOnlyCollection<IMessageTextStyle> styles)
    {
        if (styles.Count is 0) return null;

        return styles
            .Select(style => style switch
            {
                BoldTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Bold, style.Offset, style.Length),
                ItalicTextStyle => new TelegramMessageEntity(TelegramMessageEntities.Italic, style.Offset, style.Length),
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

    private TelegramReplyParameters? GetReplyParameters(IOutgoingMessage outgoingMessage)
    {
        if (outgoingMessage.Reply is null) return null;

        if (outgoingMessage.Reply.MessageIdentifier.TryGetValue(out long replyMessageTelegramId) is false)
        {
            throw new ArgumentException("Reply message telegram id is required.");
        }

        if (environmentProfileIdentifier == outgoingMessage.Reply.EnvironmentIdentifier)
        {
            return new TelegramReplyParameters(replyMessageTelegramId);
        }

        if (outgoingMessage.Reply.EnvironmentIdentifier.TryGetValue(out long replyMessageEnvironmentTelegramId) is false)
        {
            throw new ArgumentException("Reply message environment telegram id is required.");
        }

        return new TelegramReplyParameters(replyMessageTelegramId, replyMessageEnvironmentTelegramId);
    }
}
