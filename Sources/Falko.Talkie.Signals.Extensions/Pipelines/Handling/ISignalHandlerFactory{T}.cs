using Talkie.Handlers;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlerFactory<out T> : ISignalHandlerFactory where T : ISignalHandler
{
    new T Create();
}
