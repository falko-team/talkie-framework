using Talkie.Signals;

namespace Talkie.Handlers;

public interface ISignalHandler<in T> : ISignalHandler where T : Signal
{
    ValueTask HandleAsync(ISignalContext<T> context, CancellationToken cancellationToken);
}
