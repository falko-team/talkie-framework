namespace Falko.Talkie.Models.Messages.Attachments;

public static partial class MessageFileAttachmentExtensions
{
    public static IEnumerable<IMessageAttachment> OnlyFiles(this IEnumerable<IMessageAttachment> attachments)
    {
        return attachments.Where(attachment => attachment is IMessageFileAttachment);
    }
}
