using Talkie.Controllers.CommandControllers;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Handlers;

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
