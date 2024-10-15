namespace Talkie.Bridges.Telegram.Models;

public sealed class GetFile
(
    string fileId
)
{
    public readonly string FileId = fileId;
}
