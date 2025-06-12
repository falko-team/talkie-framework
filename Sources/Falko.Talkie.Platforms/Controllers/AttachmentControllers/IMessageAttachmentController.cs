using Falko.Talkie.Common;

namespace Falko.Talkie.Controllers.AttachmentControllers;

public interface IMessageAttachmentController : IController<Unit>
{
    IMessageImageAttachmentController ImageAttachment { get; }

    IMessageSickerAttachmentController SickerAttachment { get; }

    IMessageAudioAttachmentController AudioAttachment { get; }
}
