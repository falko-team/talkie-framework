using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder SkipWhile(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, CancellationToken, bool> @while)
    {
        return builder.InterceptTransient(() => new SkipWhileSignalInterceptor(@while));
    }

    public static ISignalInterceptingPipelineBuilder SkipWhile(this ISignalInterceptingPipelineBuilder builder,
        Func<Signal, bool> @while)
    {
        return builder.SkipWhile((signal, _) => @while(signal));
    }

    public static ISignalInterceptingPipelineBuilder<T> SkipWhile<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, CancellationToken, bool> @while)
        where T : Signal
    {
        return builder.InterceptTransient(() => new SkipWhileSignalInterceptor<T>(@while));
    }

    public static ISignalInterceptingPipelineBuilder<T> SkipWhile<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<T, bool> @while)
        where T : Signal
    {
        return builder.SkipWhile((signal, _) => @while(signal));
    }
}
