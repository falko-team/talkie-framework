using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> OnlySelf(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelf());
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> OnlySelf(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelf());
    }
}
