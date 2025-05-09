using Talkie.Adapters;
using Talkie.Controllers.MessageControllers;
using Talkie.Signals;

namespace Talkie.Handlers;

public static partial class SignalContextExtensions
{
    public static IMessageController ToMessageController(this ISignalContext<MessagePublishedSignal> context)
    {
        return context.To<MessageControllerAdapter, IMessageController>();
    }

    public static IMessageController ToMessageController(this ISignalContext<MessageExchangedSignal> context)
    {
        return context.To<MessageControllerAdapter, IMessageController>();
    }
}
