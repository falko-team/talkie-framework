namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public static partial class MessageImageVariantExtensions
{
    public static IEnumerable<IMessageImageVariant> GetImages(this IEnumerable<IMessageFileVariant> variants)
    {
        return variants.OfType<IMessageImageVariant>();
    }
}
