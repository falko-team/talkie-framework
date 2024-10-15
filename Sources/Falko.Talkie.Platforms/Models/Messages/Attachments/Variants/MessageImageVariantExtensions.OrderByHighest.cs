namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static IEnumerable<T> OrderByHighest<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.Count switch
        {
            0 => [],
            1 => variants,
            _ => variants.OrderByDescending(variant => variant.Area, Area.Comparer)
        };
    }

    public static IEnumerable<T> OrderByHighest<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.OrderByDescending(variant => variant.Area, Area.Comparer);
    }
}
