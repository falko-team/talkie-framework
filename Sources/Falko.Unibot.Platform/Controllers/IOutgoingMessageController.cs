using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Controllers;

public interface IOutgoingMessageController : IController<IMessage>
{
    Task SendAsync(IMessage message, CancellationToken cancellationToken = default);
}
