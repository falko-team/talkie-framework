using Talkie.Controllers.MessageControllers;
using Talkie.Handlers;
using Talkie.Models.Messages.Incoming;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Adapters;

public sealed class MessageControllerAdapter : SignalContextAdapter<MessagePublishedSignal, IMessageController>
{
    public override IMessageController Adapt(ISignalContext<MessagePublishedSignal> context)
    {
        var message = context.Signal.Message;

        var factory = message.Platform.GetMessageControllerFactory();
        var identifier = context.Signal.Message.ToGlobalMessageIdentifier();

        return factory.Create(identifier);
    }
}
