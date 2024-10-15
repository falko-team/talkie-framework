namespace Talkie.Bridges.Telegram.Models;

public sealed class Sticker
(
    string fileId,
    string fileUniqueId,
    StickerType type,
    int width,
    int height,
    bool isAnimated,
    bool isVideo,
    PhotoSize? thumbnail = null,
    string? emoji = null,
    string? setName = null,
    string? customEmojiId = null,
    bool? needsRepainting = null,
    long? fileSize = null
) : PhotoSize(fileId, fileUniqueId, width, height, fileSize)
{
    public readonly StickerType Type = type;

    public readonly bool IsAnimated = isAnimated;

    public readonly bool IsVideo = isVideo;

    public readonly PhotoSize? Thumbnail = thumbnail;

    public readonly string? Emoji = emoji;

    public readonly string? SetName = setName;

    public readonly string? CustomEmojiId = customEmojiId;

    public readonly bool? NeedsRepainting = needsRepainting;
}
