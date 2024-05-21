using Talkie.Pipelines;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Subscription Subscribe(this ISignalFlow flow, ISignalPipelineBuilder builder)
    {
        return flow.Subscribe(builder.Build());
    }

    public static Subscription Subscribe(this ISignalFlow flow, Func<ISignalInterceptingPipelineBuilder, ISignalPipelineBuilder> builderFactory)
    {
        return flow.Subscribe(builderFactory(new SignalInterceptingPipelineBuilder()));
    }
}
