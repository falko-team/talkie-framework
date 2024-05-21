using Talkie.Models.Messages;

namespace Talkie.Controllers;

public interface IOutgoingMessageController : IController<IIncomingMessage>
{
    Task<IMessage> PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default);
}
