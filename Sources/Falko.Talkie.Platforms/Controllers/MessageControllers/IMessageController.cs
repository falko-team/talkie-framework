using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.MessageControllers;

public interface IMessageController : IController<GlobalMessageIdentifier>
{
    Task<IIncomingMessage> PublishMessageAsync
    (
        IOutgoingMessage message,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default
    );

    Task<IIncomingMessage> ExchangeMessageAsync
    (
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessage message,
        CancellationToken cancellationToken = default
    );

    Task UnpublishMessageAsync
    (
        GlobalMessageIdentifier messageIdentifier,
        CancellationToken cancellationToken = default
    );
}
