namespace Talkie.Models.Identifiers;

public sealed record GlobalMessageIdentifier
(
    IProfileIdentifier EnvironmentIdentifier,
    IMessageIdentifier MessageIdentifier
);
