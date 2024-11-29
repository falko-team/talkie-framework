using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> SkipSelfPublished(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated() is false);
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> SkipSelfPublished(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated() is false);
    }
}
