namespace Talkie.Models.Identifiers;

public readonly record struct GlobalMessageIdentifier(IProfileIdentifier EnvironmentIdentifier, IMessageIdentifier MessageIdentifier);
