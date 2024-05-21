namespace Talkie.Adapters;

public static class SignalFlowAdapterCache<TAdapter, TAdapted>
    where TAdapter : class, ISignalFlowAdapter<TAdapted>, new()
    where TAdapted : notnull
{
    public static readonly TAdapter Instance = new();
}
