using Talkie.Handlers;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlingPipelineProcessor
{
    ValueTask ProcessAsync
    (
        SignalContext context,
        CancellationToken cancellationToken
    );
}
