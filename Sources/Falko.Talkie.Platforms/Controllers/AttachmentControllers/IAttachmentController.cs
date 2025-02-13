using Talkie.Common;

namespace Talkie.Controllers.AttachmentControllers;

public interface IAttachmentController : IController<Unit>
{
    IImageAttachmentController ImageAttachment { get; }
}
