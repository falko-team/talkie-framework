using Talkie.Controllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Models;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Profiles;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IMessageController GetMessageController(this ISignalContext<MessagePublishedSignal> context,
        Identifier environmentProfileIdentifier)
    {
        return context.GetMessage().GetMessageController(environmentProfileIdentifier);
    }

    public static IMessageController GetMessageController(this ISignalContext<MessageExchangedSignal> context,
        Identifier environmentProfileIdentifier)
    {
        return context.GetMessage().GetMessageController(environmentProfileIdentifier);
    }

    private static IMessageController GetMessageController(this IIncomingMessage message,
        Identifier environmentProfileIdentifier)
    {
        return message
            .Platform
            .ControllerCreator
            .CreateMessageController(environmentProfileIdentifier);
    }

    public static IMessageController GetMessageController(this ISignalContext<MessagePublishedSignal> context,
        IProfile environmentProfile)
    {
        return context.GetMessageController(environmentProfile.Identifier);
    }

    public static IMessageController GetMessageController(this ISignalContext<MessageExchangedSignal> context,
        IProfile environmentProfile)
    {
        return context.GetMessageController(environmentProfile.Identifier);
    }
}
