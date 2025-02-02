namespace Talkie.Models.Messages.Attachments.Factories;

public static class TelegramMessageImageAttachmentFactory
{
    public static IMessageImageAttachmentFactory FromUri(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        if (uri.IsAbsoluteUri is false)
        {
            throw new ArgumentException("The URI must be absolute.", nameof(uri));
        }

        return new TelegramMessageUrlImageAttachmentFactory(uri.AbsoluteUri);
    }
}
