using Talkie.Concurrent;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;
using Talkie.Sequences;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public sealed class ManySignalHandlingPipeline
(
    IEnumerable<ISignalHandler> handlers,
    ISignalInterceptingPipeline? interceptingPipeline = null
) : ISignalHandlingPipeline
{
    private readonly FrozenSequence<ISignalHandler> _handlers = handlers.ToFrozenSequence();

    public ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default)
    {
        if (interceptingPipeline?.TryTransfer(signal, out signal, cancellationToken) is false)
        {
            return ValueTask.CompletedTask;
        }

        var context = new SignalContext(flow, signal);

        return _handlers
            .Parallelize()
            .ForEachAsync((handler, scopedCancellationToken) => handler
                .HandleAsync(context, scopedCancellationToken), cancellationToken: cancellationToken)
            .AsValueTask();
    }
}
