using Falko.Talkie.Flows;
using Falko.Talkie.Handlers;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

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
