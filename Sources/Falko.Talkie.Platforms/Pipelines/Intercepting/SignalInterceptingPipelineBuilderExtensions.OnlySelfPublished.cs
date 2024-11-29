using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> OnlySelfPublished(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated());
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> OnlySelfPublished(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated());
    }
}
