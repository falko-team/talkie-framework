namespace Talkie.Models.Messages.Contents;

public readonly struct MessageTextRange(int offset, int length)
{
    public readonly int Offset { get; } = offset;

    public readonly int Length { get; } = length;
}
