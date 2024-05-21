using Falko.Talkie.Models.Messages;

namespace Falko.Talkie.Controllers;

public interface IOutgoingMessageController : IController<IIncomingMessage>
{
    Task<IMessage> PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default);
}
