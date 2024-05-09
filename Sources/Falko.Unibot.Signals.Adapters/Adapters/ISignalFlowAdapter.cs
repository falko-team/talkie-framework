using Falko.Unibot.Flows;

namespace Falko.Unibot.Adapters;

public interface ISignalFlowAdapter<out T> where T : notnull
{
    T Adapt(ISignalFlow flow);
}
