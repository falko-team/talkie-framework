namespace Talkie.Models.Messages.Attachments.Factories;

public sealed record TelegramMessageAudioAttachmentFactory : IMessageAudioAttachmentFactory
{
    public string? Alias { get; init; }

    public Stream? Stream { get; init; }
}
