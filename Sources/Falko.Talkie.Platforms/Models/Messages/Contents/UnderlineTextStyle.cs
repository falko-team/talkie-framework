namespace Talkie.Models.Messages.Contents;

public sealed record UnderlineTextStyle(int Offset, int Length) : IMessageTextStyle;
