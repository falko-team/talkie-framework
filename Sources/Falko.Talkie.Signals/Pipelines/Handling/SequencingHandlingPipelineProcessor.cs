using Talkie.Handlers;
using Talkie.Sequences;

namespace Talkie.Pipelines.Handling;

public sealed class SequencingHandlingPipelineProcessor(FrozenSequence<ISignalHandler> handlers) : ISignalHandlingPipelineProcessor
{
    public async ValueTask ProcessAsync(SignalContext context, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers.AsEnumerable())
        {
            await handler.HandleAsync(context, cancellationToken);
        }
    }
}
