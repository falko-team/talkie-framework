using Talkie.Handlers;

namespace Talkie.Piepelines2.Handling;

public interface ISignalHandlerFactory
{
    ISignalHandler Create();
}
