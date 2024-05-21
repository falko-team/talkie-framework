using Falko.Talkie.Handlers;

namespace Falko.Talkie.Adapters;

public interface ISignalContextAdapter<out T> where T : notnull
{
    T Adapt(ISignalContext context);
}
