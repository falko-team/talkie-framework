using Talkie.Validation;

namespace Talkie.Models.Identifiers;

public sealed record TelegramMessageFileAttachmentIdentifier
(
    string globalFileIdentifier,
    string localFileIdentifier
) : IMessageAttachmentIdentifier
{
    public string GlobalFileIdentifier => Assert.ArgumentNullException
        .ThrowIfNullOrEmpty(globalFileIdentifier, nameof(globalFileIdentifier));

    public string LocalFileIdentifier => Assert.ArgumentNullException
        .ThrowIfNullOrEmpty(localFileIdentifier, nameof(localFileIdentifier));
}
