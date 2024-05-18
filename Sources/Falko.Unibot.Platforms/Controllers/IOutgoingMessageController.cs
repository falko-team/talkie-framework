using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Controllers;

public interface IOutgoingMessageController : IController<IIncomingMessage>
{
    Task<IMessage> PublishMessageAsync(IMessage message, CancellationToken cancellationToken = default);
}
