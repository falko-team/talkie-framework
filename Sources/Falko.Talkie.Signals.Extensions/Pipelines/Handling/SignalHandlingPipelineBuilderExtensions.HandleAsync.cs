using Falko.Talkie.Handlers;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Handling;

public static partial class SignalHandlingPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalInterceptingPipelineBuilder builder,
        ISignalHandler handler
    )
    {
        var pipeline = builder.Build();

        return pipeline is not EmptySignalInterceptingPipeline
            ? new SignalHandlingPipelineBuilder(pipeline).HandleAsync(handler)
            : SignalHandlingPipelineBuilder.Empty.HandleAsync(handler);
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalHandler<T> handler
    ) where T : Signal
    {
        var pipeline = builder.Build();

        return pipeline is not EmptySignalInterceptingPipeline
            ? new SignalHandlingPipelineBuilder<T>(pipeline).HandleAsync(handler)
            : SignalHandlingPipelineBuilder<T>.Empty.HandleAsync(handler);
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, ValueTask> handleAsync
    )
    {
        return builder.HandleAsync(new DelegatedSignalHandler(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, ValueTask> handleAsync
    )
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ValueTask> handleAsync
    )
    {
        return builder.HandleAsync((_, _) => handleAsync());
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, ValueTask> handleAsync
    )
    {
        return builder.HandleAsync(new DelegatedSignalHandler(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, ValueTask> handleAsync
    )
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync
    (
        this ISignalHandlingPipelineBuilder builder,
        Func<ValueTask> handleAsync
    )
    {
        return builder.HandleAsync((_, _) => handleAsync());
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync(new DelegatedSignalHandler<T>(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync((_, _) => handleAsync());
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync(new DelegatedSignalHandler<T>(handleAsync));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync((signal, _) => handleAsync(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>
    (
        this ISignalHandlingPipelineBuilder<T> builder,
        Func<ValueTask> handleAsync
    ) where T : Signal
    {
        return builder.HandleAsync((_, _) => handleAsync());
    }
}
