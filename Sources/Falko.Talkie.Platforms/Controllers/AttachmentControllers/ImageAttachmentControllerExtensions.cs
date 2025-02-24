using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public static partial class ImageAttachmentControllerExtensions
{
    public static IMessageImageAttachmentFactory Build(this IImageAttachmentController controller, string uri)
    {
        return controller.Build(new Uri(uri));
    }
}
