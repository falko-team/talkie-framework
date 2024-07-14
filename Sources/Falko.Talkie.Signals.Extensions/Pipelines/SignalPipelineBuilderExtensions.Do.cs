using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Do(this ISignalInterceptingPipelineBuilder builder,
        Action<Signal, CancellationToken> @do)
    {
        return builder.Intercept(new DelegatedSignalInterceptor((signal, cancellationToken) =>
        {
            @do(signal, cancellationToken);

            return InterceptionResult.Continue();
        }));
    }

    public static ISignalInterceptingPipelineBuilder Do(this ISignalInterceptingPipelineBuilder builder,
        Action<Signal> @do)
    {
        return builder.Do((signal, _) => @do(signal));
    }

    public static ISignalInterceptingPipelineBuilder<T> Do<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Action<T, CancellationToken> @do) where T : Signal
    {
        return builder.Intercept(new DelegatedSignalInterceptor<T>((signal, cancellationToken) =>
        {
            @do(signal, cancellationToken);

            return InterceptionResult.Continue();
        }));
    }

    public static ISignalInterceptingPipelineBuilder<T> Do<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Action<T> @do) where T : Signal
    {
        return builder.Do((signal, _) => @do(signal));
    }
}
