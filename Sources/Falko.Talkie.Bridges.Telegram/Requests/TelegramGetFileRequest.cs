namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramGetFileRequest(string fileId)
{
    public string FileId => fileId;
}
