namespace Talkie.Handlers;

public interface ISignalHandler
{
    ValueTask HandleAsync(ISignalContext context, CancellationToken cancellationToken);
}
