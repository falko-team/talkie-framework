using Talkie.Handlers;

namespace Talkie.Adapters;

public interface ISignalContextAdapter<out T> where T : notnull
{
    T Adapt(ISignalContext context);
}
