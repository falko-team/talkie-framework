using Talkie.Handlers;

namespace Talkie.Piepelines2.Handling;

public interface ISignalHandlerFactory<out T> : ISignalHandlerFactory where T : ISignalHandler
{
    new T Create();
}
