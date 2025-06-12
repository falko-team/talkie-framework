using Falko.Talkie.Common;
using Falko.Talkie.Controllers.AttachmentControllers;

namespace Falko.Talkie.Platforms;

public interface IWithAttachmentControllerFactory : IWithControllerFactory<IMessageAttachmentController, Unit>;
