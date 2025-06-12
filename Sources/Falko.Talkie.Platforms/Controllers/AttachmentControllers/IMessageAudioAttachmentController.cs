using Falko.Talkie.Common;
using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments.Factories;

namespace Falko.Talkie.Controllers.AttachmentControllers;

public interface IMessageAudioAttachmentController : IController<Unit>
{
    IMessageAudioAttachmentFactory Build
    (
        IMessageAttachmentIdentifier identifier,
        MessageAudioAttachmentMetadata metadata = default
    );

    IMessageAudioAttachmentFactory Build
    (
        Uri uri,
        MessageAudioAttachmentMetadata metadata = default
    );

    IMessageAudioAttachmentFactory Build
    (
        Stream stream,
        MessageAudioAttachmentMetadata metadata = default
    );
}
