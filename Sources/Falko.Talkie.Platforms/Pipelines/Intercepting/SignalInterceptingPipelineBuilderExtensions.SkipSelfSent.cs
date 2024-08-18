using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<IncomingMessageSignal> SkipSelfSent(this ISignalInterceptingPipelineBuilder<IncomingMessageSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfSent());
    }

    public static ISignalInterceptingPipelineBuilder<IncomingMessageSignal> OnlySelfSent(this ISignalInterceptingPipelineBuilder<IncomingMessageSignal> builder)
    {
        return builder.Where(signal => signal.Message.IsSelfSent() is false);
    }
}
