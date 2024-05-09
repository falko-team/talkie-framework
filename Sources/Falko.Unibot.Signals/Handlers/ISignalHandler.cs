namespace Falko.Unibot.Handlers;

public interface ISignalHandler
{
    void Handle(ISignalContext context, CancellationToken cancellationToken);
}
