using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;

namespace Talkie.Controllers;

public static partial class ControllerCreatorExtensions
{
    public static IMessageController CreateMessageController
    (
        this IControllerCreator creator,
        GlobalMessageIdentifier identifier
    )
    {
        return creator.Create<IMessageController, GlobalMessageIdentifier>(identifier);
    }
}
