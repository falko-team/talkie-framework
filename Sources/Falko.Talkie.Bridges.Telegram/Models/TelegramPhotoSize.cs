namespace Talkie.Bridges.Telegram.Models;

public class TelegramPhotoSize
(
    string fileId,
    string fileUniqueId,
    int width,
    int height,
    long? fileSize = null
)
{
    public string FileId => fileId;

    public string FileUniqueId => fileUniqueId;

    public int Width => width;

    public int Height => height;

    public long? FileSize => fileSize;
}
