namespace Falko.Talkie.Handlers;

public interface ISignalHandler
{
    void Handle(ISignalContext context, CancellationToken cancellationToken);
}
