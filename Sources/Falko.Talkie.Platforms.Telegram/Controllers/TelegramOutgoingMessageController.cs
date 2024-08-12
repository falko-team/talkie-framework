using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Converters;
using Talkie.Models;
using Talkie.Models.Messages;
using Talkie.Platforms;
using Talkie.Validations;

namespace Talkie.Controllers;

public sealed class TelegramOutgoingMessageController(TelegramPlatform platform,
    Identifier environmentProfileIdentifier) : IOutgoingMessageController
{
    public async Task<IMessage> PublishMessageAsync(IOutgoingMessage message,
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

        return IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");
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
