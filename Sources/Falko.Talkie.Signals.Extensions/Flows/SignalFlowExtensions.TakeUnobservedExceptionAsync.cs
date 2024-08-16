using System.Runtime.CompilerServices;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals.Mixins;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Task<Exception> TakeUnobservedExceptionAsync(this ISignalFlow flow,
        CancellationToken cancellationToken = default)
    {
        return flow.TakeAsync(signals => signals
            .Where(signal => signal is IWithUnobservedExceptionSignal),
                cancellationToken)
            .ContinueWith(signal => Unsafe.As<IWithUnobservedExceptionSignal>(signal.Result).Exception,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion);
    }
}
