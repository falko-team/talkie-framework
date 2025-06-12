namespace Falko.Talkie.Bridges.Telegram.Models;

public sealed class TelegramSticker
(
    string fileId,
    string fileUniqueId,
    TelegramStickerType type,
    int width,
    int height,
    bool isAnimated,
    bool isVideo,
    TelegramPhotoSize? thumbnail = null,
    string? emoji = null,
    string? setName = null,
    string? customEmojiId = null,
    bool? needsRepainting = null,
    long? fileSize = null
) : TelegramPhotoSize(fileId, fileUniqueId, width, height, fileSize)
{
    public TelegramStickerType Type => type;

    public bool IsAnimated => isAnimated;

    public bool IsVideo => isVideo;

    public TelegramPhotoSize? Thumbnail => thumbnail;

    public string? Emoji => emoji;

    public string? SetName => setName;

    public string? CustomEmojiId => customEmojiId;

    public bool? NeedsRepainting => needsRepainting;
}
