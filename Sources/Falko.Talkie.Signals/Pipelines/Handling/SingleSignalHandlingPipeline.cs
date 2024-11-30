using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines.Handling;

public sealed class SingleSignalHandlingPipeline
(
    ISignalHandler handler,
    ISignalInterceptingPipeline? interceptingPipeline = null
) : ISignalHandlingPipeline
{
    public ValueTask TransferAsync(ISignalFlow flow, Signal signal, CancellationToken cancellationToken = default)
    {
        if (interceptingPipeline?.TryTransfer(signal, out signal, cancellationToken) is false)
        {
            return ValueTask.CompletedTask;
        }

        var context = new SignalContext(flow, signal);

        return handler.HandleAsync(context, cancellationToken);
    }
}
