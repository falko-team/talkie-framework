using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Subscription Subscribe(this ISignalFlow flow, ISignalHandlingPipelineBuilder builder)
    {
        return flow.Subscribe(builder.Build());
    }

    public static Subscription Subscribe<T>(this ISignalFlow flow, ISignalHandlingPipelineBuilder<T> builder)
        where T : Signal
    {
        return flow.Subscribe(builder.Build());
    }

    public static Subscription Subscribe(this ISignalFlow flow, Func<ISignalInterceptingPipelineBuilder,
        ISignalHandlingPipelineBuilder> builderFactory)
    {
        return flow.Subscribe(builderFactory(SignalInterceptingPipelineBuilder.Empty));
    }

    public static Subscription Subscribe<T>(this ISignalFlow flow, Func<ISignalInterceptingPipelineBuilder<T>,
        ISignalHandlingPipelineBuilder> builderFactory) where T : Signal
    {
        return flow.Subscribe(builderFactory(SignalInterceptingPipelineBuilder<T>.Empty));
    }
}
