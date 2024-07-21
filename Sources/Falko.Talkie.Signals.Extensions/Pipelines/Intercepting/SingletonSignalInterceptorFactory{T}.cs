using Talkie.Interceptors;
using Talkie.Signals;
using Talkie.Validations;

namespace Talkie.Pipelines.Intercepting;

public sealed class SingletonSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : Signal
{
    private readonly object _lock = new();

    private Func<ISignalInterceptor<T>>? _interceptorFactory;

    private ISignalInterceptor<T>? _interceptor;

    public SingletonSignalInterceptorFactory(Func<ISignalInterceptor<T>> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor<T> Create()
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
