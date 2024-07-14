using Talkie.Collections;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public sealed class SingleHandlerSignalPipeline(IEnumerable<ISignalInterceptor> interceptors, ISignalHandler handlers) : ISignalPipeline
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptors = interceptors.ToFrozenSequence();

    public ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default)
    {
        var currentSignal = signal;

        foreach (var interceptor in _interceptors)
        {
            var result = interceptor.Intercept(currentSignal, cancellationToken);

            if (result.CanContinue is false)
            {
                return ValueTask.CompletedTask;
            }

            if (result.ReplacedSignal is not null)
            {
                currentSignal = result.ReplacedSignal;
            }
        }

        var context = new SignalContext(flow, currentSignal);

        return handlers.HandleAsync(context, cancellationToken);
    }
}
