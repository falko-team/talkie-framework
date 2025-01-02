using System.Runtime.CompilerServices;
using Talkie.Concurrent;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Flows;

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
