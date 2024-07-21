using Talkie.Interceptors;
using Talkie.Validations;

namespace Talkie.Pipelines.Intercepting;

public sealed class TransientSignalInterceptorFactory : ISignalInterceptorFactory
{
    private readonly Func<ISignalInterceptor> _interceptorFactory;

    public TransientSignalInterceptorFactory(Func<ISignalInterceptor> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor Create()
    {
        var interceptor = _interceptorFactory();

        interceptor.ThrowIf().Null();

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
