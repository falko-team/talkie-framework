using Falko.Talkie.Interceptors;

namespace Falko.Talkie.Pipelines.Intercepting;

public sealed class SingletonSignalInterceptorFactory : ISignalInterceptorFactory
{
    private readonly Lock _locker = new();

    private Func<ISignalInterceptor>? _interceptorFactory;

    private ISignalInterceptor? _interceptor;

    public SingletonSignalInterceptorFactory(Func<ISignalInterceptor> interceptorFactory)
    {
        ArgumentNullException.ThrowIfNull(interceptorFactory);

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor Create()
    {
        if (_interceptor is not null) return _interceptor;

        lock (_locker)
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
