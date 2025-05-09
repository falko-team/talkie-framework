using Talkie.Controllers.CommandControllers;
using Talkie.Models.Profiles;

namespace Talkie.Controllers.CommandsControllers;

public sealed class WithoutTextTelegramCommandController : ICommandController
{
    public static readonly WithoutTextTelegramCommandController Instance = new();

    private WithoutTextTelegramCommandController() { }

    public bool IsCommand() => false;

    public bool IsCommand(bool selfRelated) => false;

    public bool IsCommand(IProfile relatedProfile) => false;

    public bool IsCommand(string commandName) => false;

    public bool IsCommand(string commandName, bool selfRelated) => false;

    public bool IsCommand(string commandName, IProfile relatedProfile) => false;

    public bool IsCommand(string[] commandNames) => false;

    public bool IsCommand(string[] commandNames, bool selfRelated) => false;

    public bool IsCommand(string[] commandNames, IProfile relatedProfile) => false;

    public bool TryGetCommand(out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(bool selfRelated, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string commandName, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string commandName, bool selfRelated, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string commandName, IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string[] commandNames, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string[] commandNames, bool selfRelated, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }

    public bool TryGetCommand(string[] commandNames, IProfile relatedProfile, out CommandMetadata commandMetadata)
    {
        commandMetadata = new CommandMetadata();
        return false;
    }
}
