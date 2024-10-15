namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static T Lowest<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.LowestOrDefault()
            ?? throw new NoVariantsFoundException();
    }

    public static T Lowest<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.LowestOrDefault()
            ?? throw new NoVariantsFoundException();
    }
}
