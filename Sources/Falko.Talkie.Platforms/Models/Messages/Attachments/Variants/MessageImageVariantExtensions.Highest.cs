namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static T Highest<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.HighestOrDefault()
            ?? throw new NoVariantsFoundException();
    }

    public static T Highest<T>(this IEnumerable<T> variants)
        where T : class, IMessageImageVariant
    {
        return variants.HighestOrDefault()
            ?? throw new NoVariantsFoundException();
    }
}
