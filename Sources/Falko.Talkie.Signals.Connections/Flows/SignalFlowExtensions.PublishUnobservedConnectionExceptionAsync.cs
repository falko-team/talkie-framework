using Talkie.Connections;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Task PublishUnobservedConnectionExceptionAsync(this ISignalFlow flow, ISignalConnection connection,
        Exception exception,
        CancellationToken cancellationToken = default)
    {
        return flow.PublishAsync(new UnobservedConnectionExceptionSignal(connection, exception), cancellationToken);
    }
}
