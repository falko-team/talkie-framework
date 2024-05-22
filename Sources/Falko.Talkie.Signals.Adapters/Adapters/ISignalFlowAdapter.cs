using Talkie.Flows;

namespace Talkie.Adapters;

public interface ISignalFlowAdapter<out T> where T : notnull
{
    T Adapt(ISignalFlow flow);
}
