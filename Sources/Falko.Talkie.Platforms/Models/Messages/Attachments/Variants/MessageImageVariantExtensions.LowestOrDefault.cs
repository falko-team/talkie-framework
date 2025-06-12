namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static T? LowestOrDefault<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.Count switch
        {
            0 => null,
            1 => variants.First(),
            _ => variants.MinBy(variant => variant.Area, Area.Comparer)
        };
    }

    public static T? LowestOrDefault<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.MinBy(variant => variant.Area, Area.Comparer);
    }
}
