using Talkie.Interceptors;
using Talkie.Validations;

namespace Talkie.Piepelines2.Intercepting;

public sealed class TransientSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : ISignalInterceptor
{
    private readonly Func<T> _interceptorFactory;

    public TransientSignalInterceptorFactory(Func<T> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public T Create()
    {
        var interceptor = _interceptorFactory();

        interceptor.ThrowIf().Null();

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
