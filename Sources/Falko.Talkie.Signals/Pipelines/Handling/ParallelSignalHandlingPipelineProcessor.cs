using Talkie.Concurrent;
using Talkie.Handlers;
using Talkie.Sequences;

namespace Talkie.Pipelines.Handling;

public sealed class ParallelSignalHandlingPipelineProcessor(FrozenSequence<ISignalHandler> handlers) : ISignalHandlingPipelineProcessor
{
    private readonly ParallelismMeter _handlersParallelismMeter = new();

    public ValueTask ProcessAsync(SignalContext context, CancellationToken cancellationToken)
    {
        return handlers.Parallelize(_handlersParallelismMeter)
            .ForEachAsync((handler, scopedCancellationToken) => handler.HandleAsync(context, scopedCancellationToken),
                cancellationToken: cancellationToken)
            .AsValueTask();
    }
}
