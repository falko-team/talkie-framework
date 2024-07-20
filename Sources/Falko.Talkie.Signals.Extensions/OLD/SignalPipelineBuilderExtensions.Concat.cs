using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Concat(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder concatBuilder)
    {
        return new SignalInterceptingPipelineBuilder(builder.CopyInterceptors()
            .Concat(concatBuilder.CopyInterceptors()));
    }

    public static ISignalInterceptingPipelineBuilder Concat(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder concatBuilder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> concatBuilderFactory)
    {
        return concatBuilderFactory(builder.Concat(concatBuilder));
    }

    public static ISignalInterceptingPipelineBuilder Concat(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> concatBuilderFactory)
    {
        return builder.Concat(concatBuilderFactory(new SignalInterceptingPipelineBuilder()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> concatBuilder) where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(builder.CopyInterceptors()
            .Concat(concatBuilder.CopyInterceptors()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> concatBuilder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> concatBuilderFactory) where T : Signal
    {
        return concatBuilderFactory(builder.Concat(concatBuilder));
    }

    public static ISignalInterceptingPipelineBuilder<T> Concat<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> concatBuilderFactory) where T : Signal
    {
        return builder.Concat(concatBuilderFactory(new SignalInterceptingPipelineBuilder<T>()));
    }
}
