using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Platforms;

public interface IWithMessageControllerFactory : IWithControllerFactory<IMessageController, GlobalMessageIdentifier>;
