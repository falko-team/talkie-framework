using Talkie.Interceptors;

namespace Talkie.Pipelines.Intercepting;

public sealed class TransientSignalInterceptorFactory : ISignalInterceptorFactory
{
    private readonly Func<ISignalInterceptor> _interceptorFactory;

    public TransientSignalInterceptorFactory(Func<ISignalInterceptor> interceptorFactory)
    {
        ArgumentNullException.ThrowIfNull(interceptorFactory);

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor Create()
    {
        var interceptor = _interceptorFactory();

        ArgumentNullException.ThrowIfNull(interceptor);

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
