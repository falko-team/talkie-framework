using Falko.Talkie.Common;
using Falko.Talkie.Controllers.AttachmentControllers;
using Falko.Talkie.Platforms;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public static partial class SignalContextExtensions
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
