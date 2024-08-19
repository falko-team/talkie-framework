using Talkie.Models;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.OutgoingMessageControllers;

public interface IOutgoingMessageController : IController<Identifier>
{
    Task<IIncomingMessage> SendMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default);
}
