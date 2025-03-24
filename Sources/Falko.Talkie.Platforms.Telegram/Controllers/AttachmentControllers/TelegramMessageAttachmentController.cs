namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageAttachmentController : IMessageAttachmentController
{
    public IMessageImageAttachmentController ImageAttachment => TelegramMessageImageAttachmentController.Instance;

    public IMessageSickerAttachmentController SickerAttachment => TelegramMessageStickerAttachmentController.Instance;

    public IMessageAudioAttachmentController AudioAttachment => TelegramMessageAudioAttachmentController.Instance;
}
