using System.Runtime.CompilerServices;
using Falko.Talkie.Concurrent;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Task<Exception> TakeUnobservedExceptionAsync
    (
        this ISignalFlow flow,
        CancellationToken cancellationToken = default
    )
    {
        return flow
            .TakeAsync(signals => signals
            .Where(signal => signal is IWithUnobservedExceptionSignal), cancellationToken)
            .InterceptOnSuccess(signal => Unsafe.As<IWithUnobservedExceptionSignal>(signal).Exception);
    }
}
