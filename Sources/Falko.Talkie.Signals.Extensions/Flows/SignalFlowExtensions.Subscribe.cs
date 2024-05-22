using Talkie.Pipelines;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Subscription Subscribe(this ISignalFlow flow, ISignalPipelineBuilder builder)
    {
        return flow.Subscribe(builder.Build());
    }

    public static Subscription Subscribe(this ISignalFlow flow, Func<ISignalInterceptingPipelineBuilder,
        ISignalPipelineBuilder> builderFactory)
    {
        return flow.Subscribe(builderFactory(new SignalInterceptingPipelineBuilder()));
    }

    public static Subscription Subscribe(this ISignalFlow flow, ISignalInterceptingPipelineBuilder builder,
        Func<ISignalInterceptingPipelineBuilder, ISignalPipelineBuilder> builderFactory)
    {
        return flow.Subscribe(builderFactory(new SignalInterceptingPipelineBuilder(builder.CopyInterceptors())));
    }

    public static Subscription Subscribe<T>(this ISignalFlow flow, ISignalInterceptingPipelineBuilder<T> builder,
        Func<ISignalInterceptingPipelineBuilder<T>, ISignalPipelineBuilder> builderFactory) where T : Signal
    {
        return flow.Subscribe(builderFactory(new SignalInterceptingPipelineBuilder<T>(builder.CopyInterceptors())));
    }
}
