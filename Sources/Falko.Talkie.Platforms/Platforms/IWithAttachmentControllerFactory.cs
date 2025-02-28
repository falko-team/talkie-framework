using Talkie.Common;
using Talkie.Controllers.AttachmentControllers;

namespace Talkie.Platforms;

public interface IWithAttachmentControllerFactory : IWithControllerFactory<IMessageAttachmentController, Unit>;
