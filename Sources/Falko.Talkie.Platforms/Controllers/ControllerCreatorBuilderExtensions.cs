using Talkie.Models;

namespace Talkie.Controllers;

public static partial class ControllerCreatorBuilderExtensions
{
    public static IControllerCreatorBuilder AddOutgoingMessageController(this IControllerCreatorBuilder builder,
        Func<Identifier, IOutgoingMessageController> controllerFactory)
    {
        builder.Add(controllerFactory);
        return builder;
    }
}
