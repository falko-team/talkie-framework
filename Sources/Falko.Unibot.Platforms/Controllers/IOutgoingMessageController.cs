using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Controllers;

public interface IOutgoingMessageController : IController<IMessage>
{
    Task PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default);
}
