using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Models.Messages.Outgoing;

namespace Falko.Talkie.Controllers.MessageControllers;

public interface IMessageController : IController<GlobalMessageIdentifier>
{
    Task<IIncomingMessage> PublishMessageAsync
    (
        IOutgoingMessage message,
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
