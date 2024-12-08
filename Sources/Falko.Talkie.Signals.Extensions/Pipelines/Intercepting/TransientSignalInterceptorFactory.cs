using Talkie.Interceptors;
using Talkie.Validation;

namespace Talkie.Pipelines.Intercepting;

public sealed class TransientSignalInterceptorFactory(Func<ISignalInterceptor> interceptorFactory)
    : ISignalInterceptorFactory
{
    private readonly Func<ISignalInterceptor> _interceptorFactory = Assert
        .ArgumentNullException.ThrowIfNull(interceptorFactory, nameof(interceptorFactory));

    public ISignalInterceptor Create()
    {
        var interceptor = _interceptorFactory();

        ArgumentNullException.ThrowIfNull(interceptor);

        return interceptor;
    }

    ISignalInterceptor ISignalInterceptorFactory.Create() => _interceptorFactory();
}
