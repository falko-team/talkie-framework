namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramAttachmentController : IAttachmentController
{
    public IImageAttachmentController ImageAttachment { get; } = new TelegramImageAttachmentController();
}
