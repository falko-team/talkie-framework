using Falko.Talkie.Concurrent;
using Falko.Talkie.Flows;
using Falko.Talkie.Handlers;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Sequences;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

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
