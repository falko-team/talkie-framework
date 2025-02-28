using Talkie.Common;
using Talkie.Controllers.AttachmentControllers;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Handlers;

public static class SignalFlowExtensions
{
    public static IMessageAttachmentController GetAttachmentController(this ISignalContext<MessagePublishedSignal> context)
    {
        return context
            .GetMessage()
            .Platform
            .GetAttachmentControllerFactory()
            .Create(Unit.Default);
    }

    public static IMessageAttachmentController GetAttachmentController(this ISignalContext<MessageExchangedSignal> context)
    {
        return context
            .GetMessage()
            .Platform
            .GetAttachmentControllerFactory()
            .Create(Unit.Default);
    }
}
