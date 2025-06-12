using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> SkipOlderThan
    (
        this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder,
        TimeSpan threshold
    )
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold) is false);
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> SkipOlderThan
    (
        this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder,
        TimeSpan threshold
    )
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold) is false);
    }
}
