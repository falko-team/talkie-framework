using Falko.Talkie.Controllers.CommandControllers;
using Falko.Talkie.Platforms;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static ICommandController ToCommandController
    (
        this ISignalContext<MessagePublishedSignal> context,
        string text
    )
    {
        ArgumentNullException.ThrowIfNull(text);

        return context
            .GetMessage()
            .Platform
            .GetCommandControllerFactory()
            .Create(text);
    }

    public static ICommandController ToCommandController
    (
        this ISignalContext<MessageExchangedSignal> context,
        string text
    )
    {
        ArgumentNullException.ThrowIfNull(text);

        return context
            .GetMessage()
            .Platform
            .GetCommandControllerFactory()
            .Create(text);
    }
}
