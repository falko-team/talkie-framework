using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder OfDynamic<T>(this ISignalInterceptingPipelineBuilder<T> builder)
        where T : Signal
    {
        return new SignalInterceptingPipelineBuilder(builder.InterceptorFactories);
    }
}
