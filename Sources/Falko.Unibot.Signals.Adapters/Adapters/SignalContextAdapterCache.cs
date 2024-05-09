namespace Falko.Unibot.Adapters;

public static class SignalContextAdapterCache<TAdapter, TAdapted>
    where TAdapter : class, ISignalContextAdapter<TAdapted>, new()
    where TAdapted : notnull
{
    public static readonly TAdapter Instance = new();
}
