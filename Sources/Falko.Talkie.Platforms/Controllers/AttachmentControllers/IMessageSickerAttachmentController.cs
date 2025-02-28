using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public interface IMessageSickerAttachmentController
{
    IMessageStickerAttachmentFactory Build(IMessageAttachmentIdentifier identifier);
}
