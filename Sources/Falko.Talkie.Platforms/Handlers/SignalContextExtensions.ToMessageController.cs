using Falko.Talkie.Adapters;
using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

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
