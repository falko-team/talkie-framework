using Talkie.Collections;
using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder mergeBuilder)
    {
        return new SignalInterceptingPipelineBuilder(new Sequence<ISignalInterceptor>
        {
            new MergeInterceptor(builder.CopyInterceptors(), mergeBuilder.CopyInterceptors())
        });
    }

    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder mergeBuilder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> mergeBuilderFactory)
    {
        return builder.Merge(mergeBuilderFactory(mergeBuilder.Copy()));
    }

    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> mergeBuilderFactory)
    {
        return builder.Merge(mergeBuilderFactory(new SignalInterceptingPipelineBuilder()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> mergeBuilder) where T : Signal
    {
        return new SignalInterceptingPipelineBuilder<T>(new Sequence<ISignalInterceptor>
        {
            new MergeInterceptor(builder.CopyInterceptors(), mergeBuilder.CopyInterceptors())
        });
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> mergeBuilder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> mergeBuilderFactory)
        where T : Signal
    {
        return builder.Merge(mergeBuilderFactory(mergeBuilder.Copy()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> mergeBuilderFactory)
        where T : Signal
    {
        return builder.Merge(mergeBuilderFactory(new SignalInterceptingPipelineBuilder<T>()));
    }
}
