using Talkie.Controllers.CommandControllers;
using Talkie.Models.Messages;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static ICommandController GetCommandController(this ISignalContext<MessagePublishedSignal> context)
    {
        var message = context.GetMessage();

        return message
            .Platform
            .GetCommandControllerFactory()
            .Create(message.GetText());
    }

    public static ICommandController GetCommandController(this ISignalContext<MessageExchangedSignal> context)
    {
        var message = context.GetMessage();

        return message
            .Platform
            .GetCommandControllerFactory()
            .Create(message.GetText());
    }
}
