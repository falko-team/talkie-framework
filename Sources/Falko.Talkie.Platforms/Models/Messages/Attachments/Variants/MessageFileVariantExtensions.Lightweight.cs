namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static T Lightweight<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.LightweightOrDefault()
            ?? throw new NoVariantsFoundException();
    }

    public static T Lightweight<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.LightweightOrDefault()
            ?? throw new NoVariantsFoundException();
    }
}
