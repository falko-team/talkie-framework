using Talkie.Interceptors;
using Talkie.Signals;
using Talkie.Validations;

namespace Talkie.Pipelines.Intercepting;

public sealed class TransientSignalInterceptorFactory<T> : ISignalInterceptorFactory<T> where T : Signal
{
    private readonly Func<ISignalInterceptor<T>> _interceptorFactory;

    public TransientSignalInterceptorFactory(Func<ISignalInterceptor<T>> interceptorFactory)
    {
        interceptorFactory.ThrowIf().Null();

        _interceptorFactory = interceptorFactory;
    }

    public ISignalInterceptor<T> Create()
    {
        var interceptor = _interceptorFactory();

        interceptor.ThrowIf().Null();

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
