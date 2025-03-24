using Talkie.Common;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

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
