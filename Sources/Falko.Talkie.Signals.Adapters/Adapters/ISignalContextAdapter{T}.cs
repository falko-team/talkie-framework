using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Adapters;

public interface ISignalContextAdapter<in TSignal, out TAdapted> : ISignalContextAdapter<TAdapted>
    where TSignal : Signal
    where TAdapted : notnull
{
    TAdapted Adapt(ISignalContext<TSignal> context);
}
