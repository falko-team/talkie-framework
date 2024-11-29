using Talkie.Exceptions;
using Talkie.Signals;

namespace Talkie.Flows;

public static partial class SignalFlowExtensions
{
    public static Task PublishUnobservedPublishingExceptionAsync
    (
        this ISignalFlow flow,
        Exception exception,
        CancellationToken cancellationToken = default
    )
    {
        if (exception is not SignalPublishingException publishingException || publishingException.Flow != flow)
        {
            publishingException = new SignalPublishingException(flow, exception);
        }

        return flow.PublishAsync(new UnobservedPublishingExceptionSignal(publishingException), cancellationToken);
    }
}
