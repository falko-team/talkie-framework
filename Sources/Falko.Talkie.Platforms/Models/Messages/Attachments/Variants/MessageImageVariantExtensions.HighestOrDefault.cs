namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static T? HighestOrDefault<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.Count switch
        {
            0 => null,
            1 => variants.First(),
            _ => variants.MaxBy(variant => variant.Area, Area.Comparer)
        };
    }

    public static T? HighestOrDefault<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.MaxBy(variant => variant.Area, Area.Comparer);
    }
}
