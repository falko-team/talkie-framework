using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramImageAttachmentController : IImageAttachmentController
{
    public IMessageImageAttachmentFactory Build(string url)
    {
        return TelegramMessageImageAttachmentFactory.FromUri(new Uri(url));
    }
}
