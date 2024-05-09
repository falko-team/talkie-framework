using Falko.Unibot.Adapters;
using Falko.Unibot.Handlers;

namespace Falko.Unibot.Flows;

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

    public static T To<T>(this ISignalContext context, ISignalContextAdapter<T> adapter) where T : notnull
    {
        return adapter.Adapt(context);
    }

    public static TAdapted To<TAdapter, TAdapted>(this ISignalContext context)
        where TAdapter : class, ISignalContextAdapter<TAdapted>, new()
        where TAdapted : notnull
    {
        return context.To(SignalContextAdapterCache<TAdapter, TAdapted>.Instance);
    }
}
