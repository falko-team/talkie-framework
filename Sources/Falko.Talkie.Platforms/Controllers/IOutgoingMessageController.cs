using Talkie.Models;
using Talkie.Models.Messages;

namespace Talkie.Controllers;

public interface IOutgoingMessageController : IController<Identifier>
{
    Task<IMessage> PublishMessageAsync(IOutgoingMessage message, CancellationToken cancellationToken = default);
}
