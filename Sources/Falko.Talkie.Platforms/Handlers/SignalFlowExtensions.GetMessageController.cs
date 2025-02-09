using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Incoming;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Handlers;

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
