using Talkie.Common;

namespace Talkie.Controllers.AttachmentControllers;

public interface IAttachmentController : IController<Nothing>
{
    IImageAttachmentController ImageAttachment { get; }
}
