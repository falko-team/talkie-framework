using Talkie.Collections;
using Talkie.Concurrent;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public sealed class ManyHandlersSignalPipeline(IEnumerable<ISignalInterceptor> interceptors, IEnumerable<ISignalHandler> handlers)
    : ISignalPipeline
{
    private readonly FrozenSequence<ISignalInterceptor> _interceptors = interceptors.ToFrozenSequence();

    private readonly FrozenSequence<ISignalHandler> _handlers = handlers.ToFrozenSequence();

    private readonly ParallelismMeter _handlersParallelismMeter = new();

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

        return _handlers.Parallelize(_handlersParallelismMeter)
            .ForEachAsync((handler, scopedCancellationToken) => handler.HandleAsync(context, scopedCancellationToken),
                cancellationToken: cancellationToken)
            .AsValueTask();
    }
}
