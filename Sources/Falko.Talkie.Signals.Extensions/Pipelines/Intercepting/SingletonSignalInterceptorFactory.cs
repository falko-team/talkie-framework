using Talkie.Interceptors;
using Talkie.Validations;

namespace Talkie.Pipelines.Intercepting;

public sealed class SingletonSignalInterceptorFactory : ISignalInterceptorFactory
{
    private readonly object _lock = new();

    private Func<ISignalInterceptor>? _interceptorFactory;

    private ISignalInterceptor? _interceptor;

    public SingletonSignalInterceptorFactory(Func<ISignalInterceptor> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor Create()
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
