using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Platforms;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IMessageController GetMessageController
    (
        this ISignalContext<MessagePublishedSignal> context,
        GlobalMessageIdentifier identifier
    )
    {
        return context.GetMessage().GetMessageController(identifier);
    }

    public static IMessageController GetMessageController
    (
        this ISignalContext<MessageExchangedSignal> context,
        GlobalMessageIdentifier identifier
    )
    {
        return context.GetMessage().GetMessageController(identifier);
    }

    private static IMessageController GetMessageController
    (
        this IIncomingMessage message,
        GlobalMessageIdentifier identifier
    )
    {
        return message.Platform.GetMessageControllerFactory().Create(identifier);
    }
}
