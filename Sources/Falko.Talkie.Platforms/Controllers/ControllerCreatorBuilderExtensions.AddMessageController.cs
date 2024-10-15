using Talkie.Controllers.MessageControllers;
using Talkie.Models;
using Talkie.Models.Identifiers;

namespace Talkie.Controllers;

public static partial class ControllerCreatorBuilderExtensions
{
    public static IControllerCreatorBuilder AddMessageController(this IControllerCreatorBuilder builder,
        Func<Identifier, IMessageController> controllerFactory)
    {
        builder.Add(controllerFactory);
        return builder;
    }
}
