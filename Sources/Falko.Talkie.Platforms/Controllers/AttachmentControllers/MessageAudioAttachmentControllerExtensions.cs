using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public static partial class MessageAudioAttachmentControllerExtensions
{
    public static IMessageAudioAttachmentFactory Build
    (
        this IMessageAudioAttachmentController controller,
        string uri,
        MessageAudioAttachmentMetadata metadata = default
    )
    {
        return controller.Build(new Uri(uri), metadata);
    }
}
