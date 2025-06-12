using System.Collections;

namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public readonly partial struct Area
{
    public static readonly IComparer<Area> Comparer = new Comparer();
}

file sealed class Comparer : IComparer<Area>, IComparer
{
    public int Compare(Area target, Area other) => target.CompareTo(other);

    public int Compare(object? target, object? other) => target is Area targetArea && other is Area otherArea
        ? Compare(targetArea, otherArea)
        : 1;
}
