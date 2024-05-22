using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder mergingBuilder)
    {
        return new SignalInterceptingPipelineBuilder(builder.CopyInterceptors()
            .Concat(mergingBuilder.CopyInterceptors()));
    }

    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder mergingBuilder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> builderFactory)
    {
        return builderFactory(builder.Merge(mergingBuilder));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> mergingBuilder) where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(builder.CopyInterceptors()
            .Concat(mergingBuilder.CopyInterceptors()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> mergingBuilder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> builderFactory) where T : Signal
    {
        return builderFactory(builder.Merge(mergingBuilder));
    }
}
