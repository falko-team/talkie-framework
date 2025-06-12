using Falko.Talkie.Models.Profiles;

namespace Falko.Talkie.Controllers.CommandControllers;

public interface ICommandController : IController<string>
{
    bool IsCommand();

    bool IsCommand(bool selfRelated);

    bool IsCommand(IProfile relatedProfile);

    bool IsCommand(string commandName);

    bool IsCommand(string commandName, bool selfRelated);

    bool IsCommand(string commandName, IProfile relatedProfile);

    bool IsCommand(string[] commandNames);

    bool IsCommand(string[] commandNames, bool selfRelated);

    bool IsCommand(string[] commandNames, IProfile relatedProfile);

    bool TryGetCommand(out CommandMetadata commandMetadata);

    bool TryGetCommand(bool selfRelated, out CommandMetadata commandMetadata);

    bool TryGetCommand(IProfile relatedProfile, out CommandMetadata commandMetadata);

    bool TryGetCommand(string commandName, out CommandMetadata commandMetadata);

    bool TryGetCommand(string commandName, bool selfRelated, out CommandMetadata commandMetadata);

    bool TryGetCommand(string commandName, IProfile relatedProfile, out CommandMetadata commandMetadata);

    bool TryGetCommand(string[] commandNames, out CommandMetadata commandMetadata);

    bool TryGetCommand(string[] commandNames, bool selfRelated, out CommandMetadata commandMetadata);

    bool TryGetCommand(string[] commandNames, IProfile relatedProfile, out CommandMetadata commandMetadata);
}
