namespace Talkie.Bridges.Telegram.Models;

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
    public readonly TelegramStickerType Type = type;

    public readonly bool IsAnimated = isAnimated;

    public readonly bool IsVideo = isVideo;

    public readonly TelegramPhotoSize? Thumbnail = thumbnail;

    public readonly string? Emoji = emoji;

    public readonly string? SetName = setName;

    public readonly string? CustomEmojiId = customEmojiId;

    public readonly bool? NeedsRepainting = needsRepainting;
}
