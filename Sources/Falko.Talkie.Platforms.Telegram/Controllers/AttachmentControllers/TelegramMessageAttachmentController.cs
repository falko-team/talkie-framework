namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageAttachmentController : IMessageAttachmentController
{
    public IMessageImageAttachmentController ImageAttachment { get; } = new TelegramMessageImageAttachmentController();

    public IMessageSickerAttachmentController SickerAttachment { get; } = new TelegramMessageStickerAttachmentController();
}
