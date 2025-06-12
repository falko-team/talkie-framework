using Falko.Talkie.Controllers.CommandControllers;
using Falko.Talkie.Models.Messages;
using Falko.Talkie.Platforms;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

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
