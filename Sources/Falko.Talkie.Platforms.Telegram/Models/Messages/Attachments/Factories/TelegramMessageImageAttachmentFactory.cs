namespace Falko.Talkie.Models.Messages.Attachments.Factories;

public sealed record TelegramMessageImageAttachmentFactory : IMessageImageAttachmentFactory
{
    public string? Alias { get; init; }

    public Stream? Stream { get; init; }
}
