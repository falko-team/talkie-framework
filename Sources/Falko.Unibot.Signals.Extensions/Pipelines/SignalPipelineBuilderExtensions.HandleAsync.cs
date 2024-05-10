using Falko.Unibot.Handlers;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, Task> handle)
    {
        return builder.Handle((signal, cancellationToken) => handle(signal, cancellationToken).Wait(cancellationToken));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalContext, Task> handle)
    {
        return builder.Handle(signal => handle(signal).Wait());
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, CancellationToken, Task> handle)
    {
        return builder.Handle((signal, cancellationToken) => handle(signal, cancellationToken).Wait(cancellationToken));
    }

    public static ISignalHandlingPipelineBuilder HandleAsync(this ISignalHandlingPipelineBuilder builder,
        Func<ISignalContext, Task> handle)
    {
        return builder.Handle(signal => handle(signal).Wait());
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, Task> handle)
        where T : Signal
    {
        return builder.Handle((signal, cancellationToken) => handle(signal, cancellationToken).Wait(cancellationToken));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, Task> handle)
        where T : Signal
    {
        return builder.Handle(signal => handle(signal).Wait());
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, CancellationToken, Task> handle)
        where T : Signal
    {
        return builder.Handle((signal, cancellationToken) => handle(signal, cancellationToken).Wait(cancellationToken));
    }

    public static ISignalHandlingPipelineBuilder<T> HandleAsync<T>(this ISignalHandlingPipelineBuilder<T> builder,
        Func<ISignalContext<T>, Task> handle)
        where T : Signal
    {
        return builder.Handle(signal => handle(signal).Wait());
    }
}
