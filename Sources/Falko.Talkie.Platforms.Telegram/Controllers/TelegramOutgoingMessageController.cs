using Falko.Talkie.Bridges.Telegram.Clients;
using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Converters;
using Falko.Talkie.Models.Messages;
using Falko.Talkie.Platforms;
using Falko.Talkie.Validations;

namespace Falko.Talkie.Controllers;

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

        var sentMessage = await platform.Client.SendMessageAsync(new SendMessage(receiverId, message.Content!),
            cancellationToken: cancellationToken);

        return IncomingMessageConverter.Convert(platform, sentMessage)
            ?? throw new InvalidOperationException("Failed to convert sent message.");
    }
}
