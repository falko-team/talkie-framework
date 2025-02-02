namespace Talkie.Bridges.Telegram.Models;


public sealed class TelegramInputMediaPhoto
(
    string media,
    string? caption = null,
    IReadOnlyCollection<TelegramMessageEntity>? captionEntities = null
) : TelegramInputMedia
{
    public override string Type => "photo";

    public string Media => media;

    public string? Caption => caption;

    public IReadOnlyCollection<TelegramMessageEntity>? CaptionEntities => captionEntities;
}
