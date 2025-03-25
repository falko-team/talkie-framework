using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public readonly record struct MessageAudioAttachmentMetadata
(
    string? Title = null,
    string? Performer = null,
    TimeSpan Duration = default,
    IMessageImageAttachmentFactory? Thumbnail = null
);
