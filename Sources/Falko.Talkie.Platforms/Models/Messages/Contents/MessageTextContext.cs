namespace Talkie.Models.Messages.Contents;

public readonly struct MessageTextContext(int offset, int length)
{
    public readonly int Offset { get; } = offset;

    public readonly int Length { get; } = length;
}
