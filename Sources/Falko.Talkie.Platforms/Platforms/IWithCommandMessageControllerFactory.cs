using Talkie.Controllers.CommandControllers;

namespace Talkie.Platforms;

public interface IWithCommandMessageControllerFactory : IWithControllerFactory<ICommandController, string>;
