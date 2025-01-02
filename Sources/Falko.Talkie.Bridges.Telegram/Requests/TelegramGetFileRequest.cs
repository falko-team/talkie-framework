using Talkie.Bridges.Telegram.Models;

namespace Talkie.Bridges.Telegram.Requests;

public sealed class TelegramGetFileRequest(string fileId) : ITelegramRequest<TelegramFile>
{
    public string FileId => fileId;
}
