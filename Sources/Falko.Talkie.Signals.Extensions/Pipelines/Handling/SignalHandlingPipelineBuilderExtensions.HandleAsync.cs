using Talkie.Handlers;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalHandlingPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalInterceptingPipelineBuilder builder,
        ISignalHandler handler)
    {
        return new SignalHandlingPipelineBuilder(builder.Build()).HandleAsync(handler);
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalHandler<T> handler) where T : Signal
    {
        return new SignalHandlingPipelineBuilder<T>(builder.Build()).HandleAsync(handler);
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, ValueTask> handleAsync)
    {
        return builder.HandleAsync(new DelegatedSignalHandler(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, ValueTask> handleAsync)
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, ValueTask> handleAsync)
    {
        return builder.HandleAsync(new DelegatedSignalHandler(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, ValueTask> handleAsync)
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, ValueTask> handleAsync) where T : Signal
    {
        return builder.HandleAsync(new DelegatedSignalHandler<T>(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, ValueTask> handleAsync) where T : Signal
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, ValueTask> handleAsync) where T : Signal
    {
        return builder.HandleAsync(new DelegatedSignalHandler<T>(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, ValueTask> handleAsync) where T : Signal
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }
}
