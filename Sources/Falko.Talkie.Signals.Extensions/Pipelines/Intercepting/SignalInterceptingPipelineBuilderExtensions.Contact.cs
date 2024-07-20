using Talkie.Piepelines2.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Concat(this ISignalInterceptingPipelineBuilder builder,
        IReadOnlySignalInterceptingPipelineBuilder concatBuilder)
    {
        var resultBuilder = builder;

        foreach (var interceptorFactory in concatBuilder.InterceptorFactories)
        {
            resultBuilder = resultBuilder.Intercept(interceptorFactory);
        }

        return resultBuilder;
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder concatBuilder) where T : Signal
    {
        var resultBuilder = builder;

        foreach (var interceptorFactory in concatBuilder.InterceptorFactories)
        {
            resultBuilder = resultBuilder.Intercept(interceptorFactory);
        }

        return resultBuilder;
    }
}
