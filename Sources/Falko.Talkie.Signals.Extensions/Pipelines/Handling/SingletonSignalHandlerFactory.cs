using Talkie.Handlers;
using Talkie.Validations;

namespace Talkie.Piepelines2.Handling;

public sealed class SingletonSignalHandlerFactory<T> : ISignalHandlerFactory<T> where T : ISignalHandler
{
    private readonly object _lock = new();

    private Func<T>? _handlerFactory;

    private T? _handler;

    public SingletonSignalHandlerFactory(Func<T> handlerFactory)
    {
        handlerFactory.ThrowIf().Null();

        _handlerFactory = handlerFactory;
    }

    public T Create()
    {
        if (_handler is not null) return _handler;

        lock (_lock)
        {
            if (_handler is not null) return _handler;

            _handler = _handlerFactory!();

            _handlerFactory = null;

            return _handler;
        }
    }

    ISignalHandler ISignalHandlerFactory.Create() => Create();
}
