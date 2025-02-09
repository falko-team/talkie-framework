using Talkie.Common;
using Talkie.Controllers.AttachmentControllers;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Handlers;

public static class SignalFlowExtensions
{
    public static IAttachmentController GetAttachmentController(this ISignalContext<MessagePublishedSignal> context)
    {
        return context
            .GetMessage()
            .Platform
            .GetAttachmentControllerFactory()
            .Create(Nothing.Instance);
    }

    public static IAttachmentController GetAttachmentController(this ISignalContext<MessageExchangedSignal> context)
    {
        return context
            .GetMessage()
            .Platform
            .GetAttachmentControllerFactory()
            .Create(Nothing.Instance);
    }
}
