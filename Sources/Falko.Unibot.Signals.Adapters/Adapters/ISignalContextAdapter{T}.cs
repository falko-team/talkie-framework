using Falko.Unibot.Handlers;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Adapters;

public interface ISignalContextAdapter<in TSignal, out TAdapted> : ISignalContextAdapter<TAdapted>
    where TSignal : Signal
    where TAdapted : notnull
{
    TAdapted Adapt(ISignalContext<TSignal> context);
}
