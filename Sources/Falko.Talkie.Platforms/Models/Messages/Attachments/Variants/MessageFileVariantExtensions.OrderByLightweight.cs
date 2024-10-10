namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static IEnumerable<T> OrderByLightweight<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.Count switch
        {
            0 => [],
            1 => variants,
            _ => variants.OrderBy(variant => variant.Size)
        };
    }

    public static IEnumerable<T> OrderByLightweight<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.OrderBy(variant => variant.Size);
    }
}
