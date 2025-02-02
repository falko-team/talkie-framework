namespace Talkie.Models.Messages.Attachments.Factories;

internal sealed class TelegramMessageUrlImageAttachmentFactory(string url) : IMessageImageAttachmentFactory
{
    public string Url => url;
}
