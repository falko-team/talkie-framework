using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public sealed class TransientSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : Signal
{
    private readonly Func<ISignalInterceptor<T>> _interceptorFactory;

    public TransientSignalInterceptorFactory(Func<ISignalInterceptor<T>> interceptorFactory)
    {
        ArgumentNullException.ThrowIfNull(interceptorFactory);

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor<T> Create()
    {
        var interceptor = _interceptorFactory();

        ArgumentNullException.ThrowIfNull(interceptor);

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
