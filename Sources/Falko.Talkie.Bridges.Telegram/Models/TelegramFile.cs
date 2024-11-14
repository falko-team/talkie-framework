namespace Talkie.Bridges.Telegram.Models;

public sealed class TelegramFile
(
    string fileId,
    string fileUniqueId,
    long? fileSize = null,
    string? filePath = null
)
{
    public readonly string FileId = fileId;

    public readonly string FileUniqueId = fileUniqueId;

    public readonly long? FileSize = fileSize;

    public readonly string? FilePath = filePath;
}
