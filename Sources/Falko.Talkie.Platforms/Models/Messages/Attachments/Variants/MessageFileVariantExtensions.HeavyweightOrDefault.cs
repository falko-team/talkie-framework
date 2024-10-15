namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static T? HeavyweightOrDefault<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.Count switch
        {
            0 => null,
            1 => variants.First(),
            _ => variants.MaxBy(variant => variant.Size)
        };
    }

    public static T? HeavyweightOrDefault<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.MaxBy(variant => variant.Size);
    }
}
