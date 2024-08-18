namespace Talkie.Models.Messages.Contents;

public sealed record BoldTextStyle(int Offset, int Length) : IMessageTextStyle;
