using Falko.Talkie.Adapters;

namespace Falko.Talkie.Handlers;

public static partial class SignalContextExtensions
{
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
