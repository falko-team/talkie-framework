using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Handlers;

public interface ISignalContext
{
    ISignalFlow Flow { get; }

    Signal Signal { get; }
}
