using Falko.Unibot.Handlers;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Adapters;

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
