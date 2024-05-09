using Falko.Unibot.Models.Messages;
using Telegram.Bot;

namespace Falko.Unibot.Controllers;

public sealed class OutgoingMessageTelegramController(ITelegramBotClient client, IMessage incomingMessage) : IOutgoingMessageController
{
    public async Task SendAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        if (incomingMessage is not XMessage.IWithEntry withEntry)
        {
            return;
        }

        if (message.Content is null)
        {
            return;
        }

        await client.SendTextMessageAsync(withEntry.Receiver.Id.GetValueOrDefault<long>(), message.Content!,
            cancellationToken: cancellationToken);
    }
}
