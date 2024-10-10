using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Attachments.Variants;

public interface IMessageFileVariant
{
    Identifier Identifier { get; }

    string? Name { get; }

    string? Type { get; }

    long Size { get; }

    Stream ToStream();
}
