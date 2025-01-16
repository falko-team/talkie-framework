namespace Talkie.Models.Messages.Features;

public sealed class SilenceMessageFeature : IMessageFeature
{
    public static readonly SilenceMessageFeature Instance = new();

    private SilenceMessageFeature() { }
}
