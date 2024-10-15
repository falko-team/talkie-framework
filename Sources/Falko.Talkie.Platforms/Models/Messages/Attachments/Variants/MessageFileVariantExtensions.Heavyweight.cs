namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageFileVariantExtensions
{
    public static T Heavyweight<T>(this IReadOnlyCollection<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.HeavyweightOrDefault()
            ?? throw new NoVariantsFoundException();
    }

    public static T Heavyweight<T>(this IEnumerable<T> variants)
        where T : class, IMessageFileVariant
    {
        return variants.HeavyweightOrDefault()
            ?? throw new NoVariantsFoundException();
    }
}
