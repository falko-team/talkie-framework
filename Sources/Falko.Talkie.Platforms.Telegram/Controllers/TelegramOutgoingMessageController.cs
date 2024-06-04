using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Converters;
using Talkie.Models.Messages;
using Talkie.Platforms;
using Talkie.Validations;

namespace Talkie.Controllers;

public sealed class TelegramOutgoingMessageController(TelegramPlatform platform, IIncomingMessage incomingMessage) : IOutgoingMessageController
{
    public async Task<IMessage> PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        message.Content.ThrowIf().Null();
        incomingMessage.ThrowIf().NotPlatform<TelegramPlatform>();

        if (incomingMessage.Entry.Environment.Id.TryGetValue(out long receiverId) is not true)
        {
            throw new ArgumentException("Environment id is required.");
        }

        var sendMessage = new SendMessage(
            receiverId,
            message.Content!,
            GetReplyParameters(message));

        var sentMessage = await platform.Client.SendMessageAsync(sendMessage,
            cancellationToken: cancellationToken);

        return IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");
    }

    private ReplyParameters? GetReplyParameters(IMessage outgoingMessage)
    {
        if (outgoingMessage.TryGetReply(out var replyMessage) is false)
        {
            return null;
        }

        if (replyMessage.TryGetIdentifier(out var replyMessageId) is false)
        {
            throw new ArgumentException("Reply message id is required.");
        }

        if (replyMessageId.TryGetValue(out long replyMessageTelegramId) is false)
        {
            throw new ArgumentException("Reply message telegram id is required.");
        }

        if (replyMessage.TryGetEntry(out var replyMessageEntry) is false)
        {
            return new ReplyParameters(replyMessageTelegramId);
        }

        if (incomingMessage.Entry.Environment.Id == replyMessageEntry.Environment.Id)
        {
            return new ReplyParameters(replyMessageTelegramId);
        }

        if (replyMessageEntry.Environment.Id.TryGetValue(out long replyMessageEnvironmentTelegramId) is false)
        {
            throw new ArgumentException("Reply message environment telegram id is required.");
        }

        return new ReplyParameters(replyMessageTelegramId, replyMessageEnvironmentTelegramId);
    }
}
