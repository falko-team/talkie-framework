using Talkie.Common;

namespace Talkie.Controllers.AttachmentControllers;

public interface IMessageAttachmentController : IController<Unit>
{
    IMessageImageAttachmentController ImageAttachment { get; }

    IMessageSickerAttachmentController SickerAttachment { get; }
}
