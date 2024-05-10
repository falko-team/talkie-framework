using Falko.Unibot.Models.Messages;
using Telegram.Bot;

namespace Falko.Unibot.Controllers;

public sealed class TelegramOutgoingMessageController(ITelegramBotClient client, IMessage incomingMessage) : IOutgoingMessageController
{
    public async Task PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        if (incomingMessage is not XMessage.IWithEntry withEntry)
        {
            return;
        }

        if (message.Content is null)
        {
            return;
        }

        await client.SendTextMessageAsync(withEntry.Receiver.Id.GetValueOrDefault<long?>() ?? 0, message.Content!,
            cancellationToken: cancellationToken);
    }
}
