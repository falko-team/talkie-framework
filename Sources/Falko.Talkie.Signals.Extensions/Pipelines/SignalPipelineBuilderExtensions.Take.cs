using Falko.Talkie.Interceptors;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Take(this ISignalInterceptingPipelineBuilder builder, int count)
    {
        return builder.Intercept(new TakeSignalInterceptor<Signal>(count));
    }

    public static ISignalInterceptingPipelineBuilder<T> Take<T>(this ISignalInterceptingPipelineBuilder<T> builder, int count)
        where T : Signal
    {
        return builder.Intercept(new TakeSignalInterceptor<T>(count));
    }
}
