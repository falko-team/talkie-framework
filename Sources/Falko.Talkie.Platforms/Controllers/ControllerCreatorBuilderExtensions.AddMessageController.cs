using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;

namespace Talkie.Controllers;

public static partial class ControllerCreatorBuilderExtensions
{
    public static IControllerCreatorBuilder AddMessageController
    (
        this IControllerCreatorBuilder builder,
        Func<GlobalMessageIdentifier, IMessageController> controllerFactory
    )
    {
        builder.Add(controllerFactory);
        return builder;
    }
}
