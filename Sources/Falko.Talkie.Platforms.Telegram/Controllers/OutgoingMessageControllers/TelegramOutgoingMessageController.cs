using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;
using Talkie.Platforms;
using Talkie.Validations;

namespace Talkie.Controllers.OutgoingMessageControllers;

public sealed class TelegramOutgoingMessageController(ISignalFlow flow,
    TelegramPlatform platform,
    Identifier environmentProfileIdentifier) : IOutgoingMessageController
{
    public async Task<IIncomingMessage> PublishMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        message.Text.ThrowIf().Null();

        if (environmentProfileIdentifier.TryGetValue(out long receiverId) is not true)
        {
            throw new ArgumentException("Environment id is required.");
        }

        var sendMessage = new SendMessage(
            receiverId,
            message.Text!,
            features.PublishSilently,
            GetReplyParameters(message));

        var sentMessage = await platform.BotApiClient.SendMessageAsync(sendMessage,
            cancellationToken: cancellationToken);

        _ = flow.PublishAsync(message.ToSignal(), cancellationToken).ContinueWith((e, _) =>
        {
            flow.PublishUnobservedPublishingExceptionAsync(e.Exception
                ?? new Exception("Failed to publish outgoing message."), cancellationToken);
        }, TaskContinuationOptions.ExecuteSynchronously, TaskContinuationOptions.OnlyOnFaulted);

        var sentIncomingMessage = IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");

        _ = flow.PublishAsync(sentIncomingMessage.ToSignal(), cancellationToken).ContinueWith((e, _) =>
        {
            flow.PublishUnobservedPublishingExceptionAsync(e.Exception
                ?? new Exception("Failed to publish self sent incoming message."), cancellationToken);
        }, TaskContinuationOptions.ExecuteSynchronously, TaskContinuationOptions.OnlyOnFaulted);

        return sentIncomingMessage;
    }

    public Task<IIncomingMessage> DeleteMessageAsync(Identifier messageId, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
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
