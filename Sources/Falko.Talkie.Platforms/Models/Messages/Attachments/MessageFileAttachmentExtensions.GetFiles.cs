namespace Talkie.Models.Messages.Attachments;

public static partial class MessageFileAttachmentExtensions
{
    public static IEnumerable<IMessageFileAttachment> GetFiles(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.OfType<IMessageFileAttachment>();
    }
}
