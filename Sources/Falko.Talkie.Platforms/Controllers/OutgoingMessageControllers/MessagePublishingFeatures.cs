namespace Talkie.Controllers.OutgoingMessageControllers;

public readonly record struct MessagePublishingFeatures(bool PublishSilently = false)
{
    public static readonly MessagePublishingFeatures Default = new();
}
