using Talkie.Models.Messages.Incoming;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> SkipOlderThan(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder,
        TimeSpan threshold)
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold) is false);
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> SkipOlderThan(this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder,
        TimeSpan threshold)
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold) is false);
    }

    private static bool IsOlderThan(this IIncomingMessage message, TimeSpan threshold)
    {
        var delta = message.ReceivedDate - message.PublishedDate;

        return delta.Ticks > 0 && delta > threshold;
    }
}
