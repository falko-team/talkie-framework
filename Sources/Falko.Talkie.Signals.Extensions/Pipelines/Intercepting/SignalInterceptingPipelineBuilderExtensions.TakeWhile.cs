using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder TakeWhile(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, bool> @while)
    {
        return builder.InterceptTransient(() => new TakeWhileSignalInterceptor(@while));
    }

    public static ISignalInterceptingPipelineBuilder TakeWhile(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, bool> @while)
    {
        return builder.TakeWhile((signal, _) => @while(signal));
    }

    public static ISignalInterceptingPipelineBuilder<T> TakeWhile<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, CancellationToken, bool> @while)
        where T : Signal
    {
        return builder.InterceptTransient(() => new TakeWhileSignalInterceptor<T>(@while));
    }

    public static ISignalInterceptingPipelineBuilder<T> TakeWhile<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, bool> @while)
        where T : Signal
    {
        return builder.TakeWhile((signal, _) => @while(signal));
    }
}
