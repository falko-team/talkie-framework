namespace Falko.Talkie.Models.Messages.Attachments;

public static partial class MessageStickerAttachmentExtensions
{
    public static IEnumerable<IMessageStickerAttachment> GetStickers(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.OfType<IMessageStickerAttachment>();
    }
}
