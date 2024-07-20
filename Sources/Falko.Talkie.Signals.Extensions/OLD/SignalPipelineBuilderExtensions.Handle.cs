using Talkie.Handlers;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder Handle(this ISignalInterceptingPipelineBuilder builder,
        Action<ISignalContext, CancellationToken> handle)
    {
        return builder.HandleAsync((context, cancellationToken) =>
        {
            handle(context, cancellationToken);

            return ValueTask.CompletedTask;
        });
    }

    public static ISignalHandlingPipelineBuilder Handle(this ISignalInterceptingPipelineBuilder builder,
        Action<ISignalContext> handle)
    {
        return builder.Handle((context, _) => handle(context));
    }

    public static ISignalHandlingPipelineBuilder Handle(this ISignalHandlingPipelineBuilder builder,
        Action<ISignalContext, CancellationToken> handle)
    {
        return builder.HandleAsync((context, cancellationToken) =>
        {
            handle(context, cancellationToken);

            return ValueTask.CompletedTask;
        });
    }

    public static ISignalHandlingPipelineBuilder Handle(this ISignalHandlingPipelineBuilder builder,
        Action<ISignalContext> handle)
    {
        return builder.Handle((signal, _) => handle(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> Handle<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Action<ISignalContext<T>, CancellationToken> handle) where T : Signal
    {
        return builder.HandleAsync((context, cancellationToken) =>
        {
            handle(context, cancellationToken);

            return ValueTask.CompletedTask;
        });
    }

    public static ISignalHandlingPipelineBuilder<T> Handle<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Action<ISignalContext<T>> handle) where T : Signal
    {
        return builder.Handle((signal, _) => handle(signal));
    }

    public static ISignalHandlingPipelineBuilder<T> Handle<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Action<ISignalContext<T>, CancellationToken> handle) where T : Signal
    {
        return builder.HandleAsync((context, cancellationToken) =>
        {
            handle(context, cancellationToken);

            return ValueTask.CompletedTask;
        });
    }

    public static ISignalHandlingPipelineBuilder<T> Handle<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Action<ISignalContext<T>> handle) where T : Signal
    {
        return builder.Handle((signal, _) => handle(signal));
    }
}
