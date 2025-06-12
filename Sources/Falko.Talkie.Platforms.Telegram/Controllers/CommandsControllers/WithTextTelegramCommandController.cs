using Falko.Talkie.Controllers.CommandControllers;
using Falko.Talkie.Models.Profiles;

namespace Falko.Talkie.Controllers.CommandsControllers;

public sealed class WithTextTelegramCommandController(IProfile relatedProfile, ReadOnlyMemory<char> text) : ICommandController
{
    public bool IsCommand() => true;

    public bool IsCommand(bool selfRelated)
    {
        if (selfRelated is false) return IsCommand();

        throw new NotImplementedException();
    }

    public bool IsCommand(IProfile relatedProfile)
    {
        throw new NotImplementedException();
    }

    public bool IsCommand(string commandName)
    {
        ArgumentException.ThrowIfNullOrEmpty(commandName);

        var commandNameLength = commandName.Length;

        var textSpan = text.Span;

        if (textSpan.Length < commandNameLength) return false;

        var commandNameSpan = textSpan[..commandNameLength];

        return commandNameSpan.Equals(commandName.AsSpan(), StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsCommand(string commandName, bool selfRelated)
    {
        return selfRelated is false
            ? IsCommand(commandName)
            : IsCommand(commandName, relatedProfile);
    }

    public bool IsCommand(string commandName, IProfile relatedProfile)
    {
        ArgumentException.ThrowIfNullOrEmpty(commandName);
        ArgumentNullException.ThrowIfNull(relatedProfile);

        var commandNameLength = commandName.Length;

        var textSpan = text.Span;

        if (textSpan.Length < commandNameLength) return false;

        if (commandName.AsSpan().StartsWith(textSpan, StringComparison.InvariantCultureIgnoreCase) is false) return false;

        var textAfterCommandNameSpan = textSpan[commandNameLength..];

        if (textAfterCommandNameSpan.Length is 0) return true;

        var firstSymbolAfterCommandName = textAfterCommandNameSpan[0];

        if (firstSymbolAfterCommandName is ' ') return true;

        if (firstSymbolAfterCommandName is not '@') return false;

        var relatedProfileName = GetProfileNickName(relatedProfile);

        if (relatedProfileName is null) return false;

        var lastSymbolAfterCommandName = textAfterCommandNameSpan.IndexOf(' ');

        textAfterCommandNameSpan = lastSymbolAfterCommandName is -1
            ? textAfterCommandNameSpan[1..]
            : textAfterCommandNameSpan[1..lastSymbolAfterCommandName];

        if (textAfterCommandNameSpan.Length < relatedProfileName.Length) return false;

        return relatedProfileName.AsSpan().StartsWith(textAfterCommandNameSpan, StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsCommand(string[] commandNames)
    {
        ArgumentNullException.ThrowIfNull(commandNames);

        if (commandNames.Length is 0) return false;

        foreach (var commandName in commandNames.AsSpan())
        {
            if (IsCommand(commandName)) return true;
        }

        return false;
    }

    public bool IsCommand(string[] commandNames, bool selfRelated)
    {
        if (selfRelated is false) return IsCommand(commandNames);

        ArgumentNullException.ThrowIfNull(commandNames);

        if (commandNames.Length is 0) return false;

        foreach (var commandName in commandNames.AsSpan())
        {
            if (IsCommand(commandName, relatedProfile)) return true;
        }

        return false;
    }

    public bool IsCommand(string[] commandNames, IProfile relatedProfile)
    {
        ArgumentNullException.ThrowIfNull(commandNames);

        if (commandNames.Length is 0) return false;

        foreach (var commandName in commandNames.AsSpan())
        {
            if (IsCommand(commandName, relatedProfile)) return true;
        }

        return false;
    }

    public bool TryGetCommand(out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(bool selfRelated, out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(string commandName, out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(string commandName, bool selfRelated, out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(string commandName, IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        throw new NotImplementedException();
    }

    public bool TryGetCommand(string[] commandNames, out CommandMetadata commandMetadata)
    {
        if (TryGetCommand(out commandMetadata) is false) return false;

        ArgumentNullException.ThrowIfNull(commandNames);

        foreach (var commandName in commandNames.AsSpan())
        {
            if (commandMetadata == commandName) return true;
        }

        commandMetadata = CommandMetadata.Invalid;
        return false;
    }

    public bool TryGetCommand(string[] commandNames, bool selfRelated, out CommandMetadata commandMetadata)
    {
        if (selfRelated is false) return TryGetCommand(commandNames, out commandMetadata);

        if (TryGetCommand(relatedProfile, out commandMetadata) is false) return false;

        ArgumentNullException.ThrowIfNull(commandNames);

        foreach (var commandName in commandNames.AsSpan())
        {
            if (commandMetadata == commandName) return true;
        }

        commandMetadata = CommandMetadata.Invalid;
        return false;
    }

    public bool TryGetCommand(string[] commandNames, IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        if (TryGetCommand(relatedProfile, out commandMetadata) is false) return false;

        ArgumentNullException.ThrowIfNull(commandNames);

        foreach (var commandName in commandNames.AsSpan())
        {
            if (commandMetadata == commandName) return true;
        }

        commandMetadata = CommandMetadata.Invalid;
        return false;
    }

    private static string? GetProfileNickName(IProfile profile)
    {
        return profile switch
        {
            IBotProfile botProfile => botProfile.NickName,
            IChatProfile groupProfile => groupProfile.NickName,
            IUserProfile userProfile => userProfile.NickName,
            _ => null
        };
    }
}
