using System.Collections.Immutable;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Concat
    (
        this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder concatBuilder
    )
    {
        return new SignalInterceptingPipelineBuilder(Concat(builder.InterceptorFactories,
            concatBuilder.InterceptorFactories));
    }

    public static ISignalInterceptingPipelineBuilder Concat
    (
        this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> concatBuilderFactory
    )
    {
        return builder.Concat(concatBuilderFactory(SignalInterceptingPipelineBuilder.Empty));
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> concatBuilder
    ) where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(Concat(builder.InterceptorFactories,
            concatBuilder.InterceptorFactories));
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>
    (
        this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> concatBuilderFactory
    ) where T : Signal
    {
        return builder.Concat(concatBuilderFactory(SignalInterceptingPipelineBuilder<T>.Empty));
    }

    private static ImmutableStack<ISignalInterceptorFactory> Concat
    (
        ImmutableStack<ISignalInterceptorFactory> targetInterceptorFactories,
        ImmutableStack<ISignalInterceptorFactory> concatInterceptorFactories
    )
    {
        var reversedConcatInterceptorFactories = new Stack<ISignalInterceptorFactory>();

        foreach (var interceptorFactory in concatInterceptorFactories)
        {
            reversedConcatInterceptorFactories.Push(interceptorFactory);
        }

        foreach (var interceptorFactory in reversedConcatInterceptorFactories)
        {
            targetInterceptorFactories = targetInterceptorFactories.Push(interceptorFactory);
        }

        return targetInterceptorFactories;
    }
}
