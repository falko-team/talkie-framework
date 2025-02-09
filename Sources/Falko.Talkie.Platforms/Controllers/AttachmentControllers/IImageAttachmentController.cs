using Talkie.Common;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public interface IImageAttachmentController : IController<Nothing>
{
    IMessageImageAttachmentFactory Build(string url);
}
