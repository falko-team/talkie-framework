using Talkie.Interceptors;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        ISignalInterceptingPipelineBuilder mergeBuilder)
    {
        return SignalInterceptingPipelineBuilder.Empty
            .InterceptSingleton(() => new MergeSignalInterceptor<Signal>(builder.Build(), mergeBuilder.Build()));
    }

    public static ISignalInterceptingPipelineBuilder Merge(this ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptingPipelineBuilder, ISignalInterceptingPipelineBuilder> mergeBuilderFactory)
    {
        return builder.Merge(mergeBuilderFactory(SignalInterceptingPipelineBuilder.Empty));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        ISignalInterceptingPipelineBuilder<T> mergeBuilder) where T : Signal
    {
        return SignalInterceptingPipelineBuilder<T>.Empty
            .InterceptSingleton(() => new MergeSignalInterceptor<T>(builder.Build(), mergeBuilder.Build()));
    }

    public static ISignalInterceptingPipelineBuilder<T> Merge<T>(this ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalInterceptingPipelineBuilder<T>> mergeBuilderFactory)
        where T : Signal
    {
        return builder.Merge(mergeBuilderFactory(SignalInterceptingPipelineBuilder<T>.Empty));
    }
}
