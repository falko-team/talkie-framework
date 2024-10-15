namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static T? LightweightOrDefault<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.Count switch
        {
            0 => null,
            1 => variants.First(),
            _ => variants.MinBy(variant => variant.Size)
        };
    }

    public static T? LightweightOrDefault<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.MinBy(variant => variant.Size);
    }
}
