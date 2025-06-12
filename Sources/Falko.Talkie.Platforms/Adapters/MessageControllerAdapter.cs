using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Handlers;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Platforms;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Adapters;

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
