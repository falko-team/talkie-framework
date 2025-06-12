using Falko.Talkie.Adapters;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static T To<T>(this ISignalFlow flow, ISignalFlowAdapter<T> adapter) where T : notnull
    {
        return adapter.Adapt(flow);
    }

    public static TAdapted To<TAdapter, TAdapted>(this ISignalFlow flow)
        where TAdapter : class, ISignalFlowAdapter<TAdapted>, new()
        where TAdapted : notnull
    {
        return flow.To(SignalFlowAdapterCache<TAdapter, TAdapted>.Instance);
    }
}
