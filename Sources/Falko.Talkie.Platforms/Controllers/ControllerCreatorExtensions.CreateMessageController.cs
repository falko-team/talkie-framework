using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;

namespace Talkie.Controllers;

public static partial class ControllerCreatorExtensions
{
    public static IMessageController CreateMessageController
    (
        this IControllerCreator creator,
        Identifier environmentProfileIdentifier
    )
    {
        return creator.Create<IMessageController, Identifier>(environmentProfileIdentifier);
    }
}
