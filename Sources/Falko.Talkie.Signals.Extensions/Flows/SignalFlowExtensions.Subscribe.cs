using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Subscription Subscribe
    (
        this ISignalFlow flow,
        IReadOnlySignalHandlingPipelineBuilder builder
    )
    {
        return flow.Subscribe(builder.Build());
    }

    public static Subscription Subscribe
    (
        this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder,
        IReadOnlySignalHandlingPipelineBuilder> builderFactory
    )
    {
        return flow.Subscribe(builderFactory(SignalInterceptingPipelineBuilder.Empty));
    }

    public static Subscription Subscribe<T>
    (
        this ISignalFlow flow,
        Func<ISignalInterceptingPipelineBuilder<T>,
        IReadOnlySignalHandlingPipelineBuilder> builderFactory
    ) where T : Signal
    {
        return flow.Subscribe(builderFactory(SignalInterceptingPipelineBuilder<T>.Empty));
    }
}
