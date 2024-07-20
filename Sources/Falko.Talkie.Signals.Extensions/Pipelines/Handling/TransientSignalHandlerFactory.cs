using Talkie.Handlers;
using Talkie.Validations;

namespace Talkie.Piepelines2.Handling;

public sealed class TransientSignalHandlerFactory<T> : ISignalHandlerFactory<T> where T : ISignalHandler
{
    private readonly Func<T> _handlerFactory;

    public TransientSignalHandlerFactory(Func<T> handlerFactory)
    {
        handlerFactory.ThrowIf().Null();

        _handlerFactory = handlerFactory;
    }

    public T Create()
    {
        var handler = _handlerFactory();

        handler.ThrowIf().Null();

        return handler;
    }

    ISignalHandler ISignalHandlerFactory.Create() => Create();
}
