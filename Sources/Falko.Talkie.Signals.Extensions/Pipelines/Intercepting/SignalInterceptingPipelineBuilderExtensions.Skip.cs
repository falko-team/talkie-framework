using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Skip(this ISignalInterceptingPipelineBuilder builder, int count)
    {
        return builder.InterceptTransient(() => new SkipSignalInterceptor(count));
    }

    public static ISignalInterceptingPipelineBuilder<T> Skip<T>(this ISignalInterceptingPipelineBuilder<T> builder, int count)
        where T : Signal
    {
        return builder.InterceptTransient(() => new SkipSignalInterceptor<T>(count));
    }
}
