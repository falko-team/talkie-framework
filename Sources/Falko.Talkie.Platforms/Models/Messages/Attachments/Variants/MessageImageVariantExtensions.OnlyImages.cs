namespace Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static IEnumerable<IMessageFileVariant> OnlyImages(this IEnumerable<IMessageFileVariant> variants)
    {
        return variants.Where(variant => variant is IMessageImageVariant);
    }
}
