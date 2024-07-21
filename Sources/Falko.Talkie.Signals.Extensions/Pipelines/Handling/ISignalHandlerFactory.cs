using Talkie.Handlers;

namespace Talkie.Pipelines.Handling;

public interface ISignalHandlerFactory
{
    ISignalHandler Create();
}
