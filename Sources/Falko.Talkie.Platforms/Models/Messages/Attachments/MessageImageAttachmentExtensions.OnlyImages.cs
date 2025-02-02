namespace Talkie.Models.Messages.Attachments;

public static partial class MessageImageAttachmentExtensions
{
    public static IEnumerable<IMessageAttachment> OnlyImages(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.Where(attachment => attachment is IMessageImageAttachment);
    }
}
