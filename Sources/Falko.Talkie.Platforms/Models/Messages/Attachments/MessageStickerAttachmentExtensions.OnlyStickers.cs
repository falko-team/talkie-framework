namespace Talkie.Models.Messages.Attachments;

public static partial class MessageStickerAttachmentExtensions
{
    public static IEnumerable<IMessageAttachment> OnlyStickers(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.Where(attachment => attachment is IMessageStickerAttachment);
    }
}
