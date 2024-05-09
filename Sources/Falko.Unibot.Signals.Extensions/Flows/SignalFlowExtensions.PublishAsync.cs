using Falko.Unibot.Signals;

namespace Falko.Unibot.Flows;

public static partial class SignalFlowExtensions
{
    public static Task PublishAsync<T>(this ISignalFlow flow, CancellationToken cancellationToken = default)
        where T : Signal, new()
    {
        return flow.PublishAsync(SignalCache<T>.Instance, cancellationToken);
    }
}
