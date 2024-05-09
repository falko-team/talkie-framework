using Falko.Unibot.Flows;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public interface ISignalContext
{
    ISignalFlow Flow { get; }

    Signal Signal { get; }
}
