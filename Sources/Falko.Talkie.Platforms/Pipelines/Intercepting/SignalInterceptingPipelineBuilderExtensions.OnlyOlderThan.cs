using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> OnlyOlderThan
    (
        this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder,
        TimeSpan threshold
    )
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold));
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> OnlyOlderThan
    (
        this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder,
        TimeSpan threshold
    )
    {
        return builder.Where(signal => signal.Message.IsOlderThan(threshold));
    }
}
