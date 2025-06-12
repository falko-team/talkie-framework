using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;

namespace Falko.Talkie.Controllers.AttachmentControllers;

public interface IMessageSickerAttachmentController
{
    IMessageStickerAttachmentFactory Build(IMessageAttachmentIdentifier identifier);
}
