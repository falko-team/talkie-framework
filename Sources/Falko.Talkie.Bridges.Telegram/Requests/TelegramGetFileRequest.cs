namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramGetFileRequest(string fileId)
{
    public readonly string FileId = fileId;
}
