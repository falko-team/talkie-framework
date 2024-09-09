namespace Talkie.Controllers.MessageControllers;

public readonly record struct MessagePublishingFeatures(bool PublishSilently = false)
{
    public static readonly MessagePublishingFeatures Default = new();
}
