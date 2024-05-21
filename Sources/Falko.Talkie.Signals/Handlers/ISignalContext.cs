using Falko.Talkie.Flows;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public interface ISignalContext
{
    ISignalFlow Flow { get; }

    Signal Signal { get; }
}
