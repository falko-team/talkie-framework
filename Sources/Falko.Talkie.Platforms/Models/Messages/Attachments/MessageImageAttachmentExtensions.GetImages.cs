namespace Talkie.Models.Messages.Attachments;

public static partial class MessageImageAttachmentExtensions
{
    public static IEnumerable<IMessageImageAttachment> GetImages(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.OfType<IMessageImageAttachment>();
    }
}
