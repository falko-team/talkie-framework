namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public readonly partial struct Area
{
    public static readonly Area Empty = new();

    public bool IsEmpty => this == Empty;
}
