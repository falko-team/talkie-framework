using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public static partial class MessageImageAttachmentControllerExtensions
{
    public static IMessageImageAttachmentFactory Build(this IMessageImageAttachmentController controller, string uri)
    {
        return controller.Build(new Uri(uri));
    }
}
