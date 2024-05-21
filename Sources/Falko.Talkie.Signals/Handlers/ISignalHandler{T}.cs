using Talkie.Signals;

namespace Talkie.Handlers;

public interface ISignalHandler<in T> : ISignalHandler where T : Signal
{
    void Handle(ISignalContext<T> context, CancellationToken cancellationToken);
}
