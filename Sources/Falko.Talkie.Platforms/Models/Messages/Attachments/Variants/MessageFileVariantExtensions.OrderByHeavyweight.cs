namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static IEnumerable<T> OrderByHeavyweight<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.Count switch
        {
            0 => [],
            1 => variants,
            _ => variants.OrderByDescending(variant => variant.Size)
        };
    }

    public static IEnumerable<T> OrderByHeavyweight<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.OrderByDescending(variant => variant.Size);
    }
}
