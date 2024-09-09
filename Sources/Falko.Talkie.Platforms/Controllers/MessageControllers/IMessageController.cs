using Talkie.Models;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.MessageControllers;

public interface IMessageController : IController<Identifier>
{
    Task<IIncomingMessage> PublishMessageAsync(IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default);

    Task<IIncomingMessage> ExchangeMessageAsync(GlobalIdentifier messageIdentifier,
        IOutgoingMessage message,
        CancellationToken cancellationToken = default);

    Task UnpublishMessageAsync(GlobalIdentifier messageIdentifier,
        CancellationToken cancellationToken = default);
}
