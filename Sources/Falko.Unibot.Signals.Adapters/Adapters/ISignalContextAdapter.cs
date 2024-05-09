using Falko.Unibot.Handlers;

namespace Falko.Unibot.Adapters;

public interface ISignalContextAdapter<out T> where T : notnull
{
    T Adapt(ISignalContext context);
}
