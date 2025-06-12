using Falko.Talkie.Common;
using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;

namespace Falko.Talkie.Controllers.AttachmentControllers;

public interface IMessageImageAttachmentController : IController<Unit>
{
    IMessageImageAttachmentFactory Build(IMessageAttachmentIdentifier identifier);

    IMessageImageAttachmentFactory Build(Uri uri);

    IMessageImageAttachmentFactory Build(Stream stream);
}
