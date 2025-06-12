using Falko.Talkie.Controllers.CommandControllers;

namespace Falko.Talkie.Platforms;

public interface IWithCommandMessageControllerFactory : IWithControllerFactory<ICommandController, string>;
