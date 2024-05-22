using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Throttle(this ISignalInterceptingPipelineBuilder builder, TimeSpan delay)
    {
        return builder.Intercept(new ThrottleSignalInterceptor<Signal>(delay));
    }

    public static ISignalInterceptingPipelineBuilder<T> Throttle<T>(this ISignalInterceptingPipelineBuilder<T> builder, TimeSpan delay)
        where T : Signal
    {
        return builder.Intercept(new ThrottleSignalInterceptor<T>(delay));
    }
}
