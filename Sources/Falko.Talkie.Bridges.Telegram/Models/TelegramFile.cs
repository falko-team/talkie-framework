namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramFile
(
    string fileId,
    string fileUniqueId,
    long? fileSize = null,
    string? filePath = null
)
{
    public string FileId => fileId;

    public string FileUniqueId => fileUniqueId;

    public long? FileSize => fileSize;

    public string? FilePath => filePath;
}
