using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder InterceptTransient
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptor> interceptorFactory
    )
    {
        return builder.Intercept(new TransientSignalInterceptorFactory(interceptorFactory));
    }

    public static ISignalInterceptingPipelineBuilder<T> InterceptTransient<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptor<T>> interceptorFactory
    ) where T : Signal
    {
        return builder.Intercept(new TransientSignalInterceptorFactory<T>(interceptorFactory));
    }
}
