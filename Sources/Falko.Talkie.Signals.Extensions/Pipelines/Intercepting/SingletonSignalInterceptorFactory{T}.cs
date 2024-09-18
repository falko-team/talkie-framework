using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public sealed class SingletonSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : Signal
{
    private readonly object _lock = new();

    private Func<ISignalInterceptor<T>>? _interceptorFactory;

    private ISignalInterceptor<T>? _interceptor;

    public SingletonSignalInterceptorFactory(Func<ISignalInterceptor<T>> interceptorFactory)
    {
        ArgumentNullException.ThrowIfNull(interceptorFactory);

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

            ArgumentNullException.ThrowIfNull(_interceptor);

            return _interceptor;
        }
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => Create();
}
