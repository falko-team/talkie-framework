using Falko.Talkie.Flows;

namespace Falko.Talkie.Adapters;

public interface ISignalFlowAdapter<out T> where T : notnull
{
    T Adapt(ISignalFlow flow);
}
