using System.Diagnostics.CodeAnalysis;
using Talkie.Models.Profiles;

namespace Talkie.Controllers.CommandControllers;

public readonly ref struct CommandMetadata : IEquatable<CommandMetadata>, IEquatable<string>, IEquatable<IProfile>
{
    public static CommandMetadata Invalid => new();

    public readonly bool IsValidMetadata;

    public readonly string CommandName;

    public readonly IProfile? RelatedProfile;

    public CommandMetadata()
    {
        CommandName = string.Empty;
    }

    public CommandMetadata(string commandName, IProfile? relatedProfile = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(commandName);

        IsValidMetadata = true;
        CommandName = commandName;
        RelatedProfile = relatedProfile;
    }

    public override int GetHashCode() => IsValidMetadata ? HashCode.Combine(CommandName, RelatedProfile) : 0;

    public override bool Equals([NotNullWhen(true)] object? obj) => obj switch
    {
        null => false,
        string otherString => this == otherString,
        IProfile otherProfile => this == otherProfile,
        _ => false
    };

    public bool Equals(CommandMetadata other) => this == other;

    public bool Equals(string? other) => other is not null && this == other;

    public bool Equals(IProfile? other) => other is not null && this == other;

    public override string ToString()
    {
        return IsValidMetadata
            ? $"{nameof(CommandMetadata)} {{ {nameof(CommandName)} = {CommandName}, {nameof(RelatedProfile)} = {RelatedProfile} }}"
            : $"{nameof(CommandMetadata)} {{ }}";
    }

    public static bool operator ==(CommandMetadata left, CommandMetadata right)
    {
        var leftIsValid = left.IsValidMetadata;
        var rightIsValid = right.IsValidMetadata;

        if (leftIsValid != rightIsValid) return false;

        if (leftIsValid is false && rightIsValid is false) return true;

        return left.CommandName.Equals(right.CommandName, StringComparison.InvariantCultureIgnoreCase)
            && Equals(left.RelatedProfile, right.RelatedProfile);
    }

    public static bool operator !=(CommandMetadata left, CommandMetadata right)
    {
        var leftIsValid = left.IsValidMetadata;
        var rightIsValid = right.IsValidMetadata;

        if (leftIsValid != rightIsValid) return true;

        if (leftIsValid is false && rightIsValid is false) return false;

        return left.CommandName.Equals(right.CommandName, StringComparison.InvariantCultureIgnoreCase) is false
            || Equals(left.RelatedProfile, right.RelatedProfile) is false;
    }

    public static bool operator ==(CommandMetadata left, string right)
    {
        return left.IsValidMetadata && left.CommandName.Equals(right, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool operator !=(CommandMetadata left, string right)
    {
        return left.IsValidMetadata is false || left.CommandName.Equals(right, StringComparison.InvariantCultureIgnoreCase) is false;
    }

    public static bool operator ==(CommandMetadata left, IProfile right)
    {
        var profile = left.RelatedProfile;

        return profile is not null && profile.Equals(right);
    }

    public static bool operator !=(CommandMetadata left, IProfile right)
    {
        var profile = left.RelatedProfile;

        return profile is null || profile.Equals(right);
    }
}
