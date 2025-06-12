namespace Falko.Talkie.Models.Identifiers;

public sealed record TelegramMessageFileAttachmentIdentifier
(
    string GlobalFileIdentifier,
    string LocalFileIdentifier
) : IMessageAttachmentIdentifier;
