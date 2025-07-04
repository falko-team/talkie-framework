using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> SkipSelfRelated(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated() is false);
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> SkipSelfRelated(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated() is false);
    }
}
