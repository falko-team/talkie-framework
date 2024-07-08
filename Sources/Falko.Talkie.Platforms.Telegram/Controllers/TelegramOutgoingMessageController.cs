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
        message.Text.ThrowIf().Null();
        incomingMessage.ThrowIf().NotPlatform<TelegramPlatform>();

        if (incomingMessage.EnvironmentProfile.Identifier.TryGetValue(out long receiverId) is not true)
        {
            throw new ArgumentException("Environment id is required.");
        }

        var sendMessage = new SendMessage(
            receiverId,
            message.Text!,
            GetReplyParameters(message));

        var sentMessage = await platform.BotApiClient.SendMessageAsync(sendMessage,
            cancellationToken: cancellationToken);

        return IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");
    }

    private ReplyParameters? GetReplyParameters(IMessage outgoingMessage)
    {
        if (outgoingMessage.Reply is null)
        {
            return null;
        }

        if (outgoingMessage.Reply is not IIncomingMessage replyMessage)
        {
            throw new ArgumentException("Reply message id is required.");
        }

        if (replyMessage.Identifier.TryGetValue(out long replyMessageTelegramId) is false)
        {
            throw new ArgumentException("Reply message telegram id is required.");
        }

        if (incomingMessage.EnvironmentProfile.Identifier == replyMessage.EnvironmentProfile.Identifier)
        {
            return new ReplyParameters(replyMessageTelegramId);
        }

        if (replyMessage.EnvironmentProfile.Identifier.TryGetValue(out long replyMessageEnvironmentTelegramId) is false)
        {
            throw new ArgumentException("Reply message environment telegram id is required.");
        }

        return new ReplyParameters(replyMessageTelegramId, replyMessageEnvironmentTelegramId);
    }
}
