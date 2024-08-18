namespace Talkie.Models.Messages.Contents;

public sealed record StrikethroughTextStyle(int Offset, int Length) : IMessageTextStyle;
