using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Take
    (
        this ISignalInterceptingPipelineBuilder builder,
        int count
    )
    {
        return builder.InterceptTransient(() => new TakeSignalInterceptor(count));
    }

    public static ISignalInterceptingPipelineBuilder<T> Take<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        int count
    ) where T : Signal
    {
        return builder.InterceptTransient(() => new TakeSignalInterceptor<T>(count));
    }
}
