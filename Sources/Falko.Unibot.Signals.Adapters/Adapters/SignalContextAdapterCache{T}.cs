using Falko.Unibot.Signals;

namespace Falko.Unibot.Adapters;

public static class SignalContextAdapterCache<TAdapter, TAdapted, TSignal>
    where TAdapter : class, ISignalContextAdapter<TSignal, TAdapted>, new()
    where TAdapted : notnull
    where TSignal : Signal
{
    public static readonly TAdapter Instance = new();
}
