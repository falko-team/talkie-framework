using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;
using Talkie.Platforms;
using Talkie.Sequences;
using Talkie.Validations;

namespace Talkie.Controllers.MessageControllers;

public sealed class TelegramMessageController(ISignalFlow flow,
    TelegramPlatform platform,
    Identifier environmentProfileIdentifier) : IMessageController
{
    public async Task<IIncomingMessage> PublishMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        message.Content.ThrowIf().Null();

        if (environmentProfileIdentifier.TryGetValue(out long receiverId) is not true)
        {
            throw new ArgumentException("Environment id is required.");
        }

        var sendMessage = new SendMessage(
            receiverId,
            message.Content,
            GetEnitites(message.Content.Styles),
            features.PublishSilently,
            GetReplyParameters(message));

        var sentMessage = await platform.BotApiClient.SendMessageAsync(sendMessage,
            cancellationToken: cancellationToken);

        var sentIncomingMessage = IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");

        _ = flow.PublishAsync(sentIncomingMessage.ToMessageReceivedSignal(), cancellationToken).ContinueWith((e, _) =>
        {
            flow.PublishUnobservedPublishingExceptionAsync(e.Exception
                ?? new Exception("Failed to publish self sent incoming message."), cancellationToken);
        }, TaskContinuationOptions.ExecuteSynchronously, TaskContinuationOptions.OnlyOnFaulted);

        return sentIncomingMessage;
    }

    public async Task<IIncomingMessage> ExchangeMessageAsync(GlobalIdentifier messageIdentifier, IOutgoingMessage message,
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

        var editMessageText = new EditMessageText(
            message.Content.Text,
            entities: GetEnitites(message.Content.Styles),
            messageId: messageTelegramId,
            chatId: environmentTelegramId);

        var sentMessage = await platform.BotApiClient.EditMessageTextAsync(editMessageText,
            cancellationToken: cancellationToken);

        var sentIncomingMessage = IncomingMessageConverter.Convert(platform, sentMessage)
                                  ?? throw new InvalidOperationException("Failed to convert sent message.");

        _ = flow.PublishAsync(sentIncomingMessage.ToMessageReceivedSignal(), cancellationToken).ContinueWith((e, _) =>
        {
            flow.PublishUnobservedPublishingExceptionAsync(e.Exception
                                                           ?? new Exception("Failed to publish self sent incoming message."), cancellationToken);
        }, TaskContinuationOptions.ExecuteSynchronously, TaskContinuationOptions.OnlyOnFaulted);

        return sentIncomingMessage;
    }

    public async Task UnpublishMessageAsync(GlobalIdentifier messageIdentifier, CancellationToken cancellationToken = default)
    {
        if (messageIdentifier.EnvironmentIdentifier.TryGetValue(out long environmentTelegramId) is false)
        {
            throw new ArgumentException("Environment id is required.");
        }

        if (messageIdentifier.MessageIdentifier.TryGetValue(out long messageTelegramId) is false)
        {
            throw new ArgumentException("Message id is required.");
        }

        var deleteMessage = new DeleteMessage(messageTelegramId, environmentTelegramId);

        if (await platform.BotApiClient.DeleteMessageAsync(deleteMessage, cancellationToken) is false)
        {
            throw new InvalidOperationException("Message was not found.");
        }
    }

    private IReadOnlyCollection<MessageEntity>? GetEnitites(IReadOnlyCollection<IMessageTextStyle> styles)
    {
        if (styles.Count is 0) return null;

        return styles
            .Select(style => style switch
            {
                BoldTextStyle => new MessageEntity(MessageEntities.Bold, style.Offset, style.Length),
                ItalicTextStyle => new MessageEntity(MessageEntities.Italic, style.Offset, style.Length),
                UnderlineTextStyle => new MessageEntity(MessageEntities.Underline, style.Offset, style.Length),
                StrikethroughTextStyle => new MessageEntity(MessageEntities.Strikethrough, style.Offset, style.Length),
                MonospaceTextStyle => new MessageEntity(MessageEntities.Code, style.Offset, style.Length),
                QuotationTextStyle => new MessageEntity(MessageEntities.BlockQuote, style.Offset, style.Length),
                _ => null
            })
            .Where(entity => entity is not null)
            .Cast<MessageEntity>()
            .ToFrozenSequence();
    }

    private ReplyParameters? GetReplyParameters(IOutgoingMessage outgoingMessage)
    {
        if (outgoingMessage.Reply is null)
        {
            return null;
        }

        if (outgoingMessage.Reply.MessageIdentifier.TryGetValue(out long replyMessageTelegramId) is false)
        {
            throw new ArgumentException("Reply message telegram id is required.");
        }

        if (environmentProfileIdentifier == outgoingMessage.Reply.EnvironmentIdentifier)
        {
            return new ReplyParameters(replyMessageTelegramId);
        }

        if (outgoingMessage.Reply.EnvironmentIdentifier.TryGetValue(out long replyMessageEnvironmentTelegramId) is false)
        {
            throw new ArgumentException("Reply message environment telegram id is required.");
        }

        return new ReplyParameters(replyMessageTelegramId, replyMessageEnvironmentTelegramId);
    }
}
