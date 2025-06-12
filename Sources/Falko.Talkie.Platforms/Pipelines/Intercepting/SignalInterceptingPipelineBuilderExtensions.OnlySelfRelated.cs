using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> OnlySelfRelated(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated());
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> OnlySelfRelated(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfRelated());
    }
}
