using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder InterceptSingleton
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptor> interceptorFactory
    )
    {
        return builder.Intercept(new SingletonSignalInterceptorFactory(interceptorFactory));
    }

    public static ISignalInterceptingPipelineBuilder<T> InterceptSingleton<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptor<T>> interceptorFactory
    ) where T : Signal
    {
        return builder.Intercept(new SingletonSignalInterceptorFactory<T>(interceptorFactory));
    }
}
