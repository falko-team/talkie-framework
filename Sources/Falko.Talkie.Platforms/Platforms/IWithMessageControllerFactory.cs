using Talkie.Controllers.MessageControllers;
using Talkie.Models.Identifiers;

namespace Talkie.Platforms;

public interface IWithMessageControllerFactory : IWithControllerFactory<IMessageController, GlobalMessageIdentifier>;
