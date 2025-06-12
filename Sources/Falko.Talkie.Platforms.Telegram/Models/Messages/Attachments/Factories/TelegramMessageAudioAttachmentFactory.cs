namespace Falko.Talkie.Models.Messages.Attachments.Factories;

public sealed record TelegramMessageAudioAttachmentFactory : IMessageAudioAttachmentFactory
{
    public string? Alias { get; init; }

    public Stream? Stream { get; init; }

    public string? Title { get; init; }

    public string? Performer { get; init; }

    public TimeSpan Duration { get; init; }

    public TelegramMessageImageAttachmentFactory? Thumbnail { get; init; }
}
