using Talkie.Models;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.OutgoingMessageControllers;

public interface IOutgoingMessageController : IController<Identifier>
{
    Task<IIncomingMessage> PublishMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default);

    Task<IIncomingMessage> DeleteMessageAsync(Identifier messageId,
        CancellationToken cancellationToken = default);
}
