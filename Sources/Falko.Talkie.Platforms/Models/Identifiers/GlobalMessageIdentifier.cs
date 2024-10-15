namespace Talkie.Models.Identifiers;

public sealed record GlobalMessageIdentifier(Identifier EnvironmentIdentifier, Identifier MessageIdentifier);
