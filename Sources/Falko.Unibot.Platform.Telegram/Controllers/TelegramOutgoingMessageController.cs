using Falko.Unibot.Bridges.Telegram.Clients;
using Falko.Unibot.Bridges.Telegram.Models;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Platforms;

namespace Falko.Unibot.Controllers;

public sealed class TelegramOutgoingMessageController(ITelegramBotApiClient client, IMessage incomingMessage) : IOutgoingMessageController
{
    public async Task PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        if (incomingMessage.OfPlatform<TelegramPlatform>() is false) return;

        if (message.Content is null) return;

        if (incomingMessage.WithEntry()?.Receiver.Id.TryGetValue(out long id) is true)
        {
            await client.SendMessageAsync(new SendMessage(id, message.Content),
                cancellationToken: cancellationToken);
        }
    }
}
