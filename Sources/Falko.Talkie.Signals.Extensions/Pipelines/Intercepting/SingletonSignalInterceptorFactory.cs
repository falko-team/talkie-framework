using Talkie.Interceptors;
using Talkie.Validations;

namespace Talkie.Piepelines2.Intercepting;

public sealed class SingletonSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : ISignalInterceptor
{
    private readonly object _lock = new();

    private Func<T>? _interceptorFactory;

    private T? _interceptor;

    public SingletonSignalInterceptorFactory(Func<T> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public T Create()
    {
        if (_interceptor is not null) return _interceptor;

        lock (_lock)
        {
            if (_interceptor is not null) return _interceptor;

            _interceptor = _interceptorFactory!();

            _interceptorFactory = null;

            _interceptor.ThrowIf().Null();

            return _interceptor;
        }
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => Create();
}
