namespace Talkie.Bridges.Telegram.Models;

public class PhotoSize(
    string fileId,
    string fileUniqueId,
    int width,
    int height,
    long? fileSize = null
)
{
    public readonly string FileId = fileId;

    public readonly string FileUniqueId = fileUniqueId;

    public readonly int Width = width;

    public readonly int Height = height;

    public readonly long? FileSize = fileSize;
}
