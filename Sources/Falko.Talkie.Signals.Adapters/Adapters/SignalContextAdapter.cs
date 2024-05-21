using Falko.Talkie.Handlers;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Adapters;

public abstract class SignalContextAdapter<TSignal, TAdapted> : ISignalContextAdapter<TSignal, TAdapted>
    where TSignal : Signal
    where TAdapted : notnull
{
    public abstract TAdapted Adapt(ISignalContext<TSignal> context);

    TAdapted ISignalContextAdapter<TAdapted>.Adapt(ISignalContext context)
    {
        if (context is ISignalContext<TSignal> castedContext is false)
        {
            throw new InvalidCastException();
        }

        return Adapt(castedContext);
    }
}
