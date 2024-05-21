namespace Falko.Talkie.Models.Messages;

public interface IIncomingMessage : Message.IWithPlatform, Message.IWithIdentifier, Message.IWithEntry;
