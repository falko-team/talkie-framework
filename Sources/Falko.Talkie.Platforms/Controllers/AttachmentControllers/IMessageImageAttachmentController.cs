using Talkie.Common;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public interface IMessageImageAttachmentController : IController<Unit>
{
    IMessageImageAttachmentFactory Build(IMessageAttachmentIdentifier identifier);

    IMessageImageAttachmentFactory Build(Uri uri);

    IMessageImageAttachmentFactory Build(Stream stream);
}
