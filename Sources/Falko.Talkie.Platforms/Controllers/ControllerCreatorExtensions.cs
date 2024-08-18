using Talkie.Controllers.OutgoingMessageControllers;
using Talkie.Models;

namespace Talkie.Controllers;

public static partial class ControllerCreatorExtensions
{
    public static IOutgoingMessageController CreateOutgoingMessageController(this IControllerCreator creator,
        Identifier environmentProfileIdentifier)
    {
        return creator.Create<IOutgoingMessageController, Identifier>(environmentProfileIdentifier);
    }
}
