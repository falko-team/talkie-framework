using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Pipelines;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<IncomingMessageSignal> SkipOlderThan(this ISignalInterceptingPipelineBuilder<IncomingMessageSignal> builder,
        TimeSpan threshold)
    {
        return builder.Where(signal => signal.Message.Received - signal.Message.Sent <= threshold);
    }
}
