namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static IEnumerable<T> OrderByLowest<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.Count switch
        {
            0 => [],
            1 => variants,
            _ => variants.OrderBy(variant => variant.Area, Area.Comparer)
        };
    }

    public static IEnumerable<T> OrderByLowest<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.OrderBy(variant => variant.Area, Area.Comparer);
    }
}
