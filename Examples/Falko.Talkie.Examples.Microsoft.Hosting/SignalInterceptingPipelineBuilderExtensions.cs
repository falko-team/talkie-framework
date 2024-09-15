using Talkie.Concurrent;
using Talkie.Controllers.MessageControllers;
using Talkie.Handlers;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Outgoing;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;

namespace Talkie.Examples;

public static class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalHandlingPipelineBuilder<MessagePublishedSignal> HandleThrottleAsync(this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder,
        Func<ISignalContext<MessagePublishedSignal>, CancellationToken, ValueTask> handler,
        int limit = 3,
        TimeSpan delay = default,
        int parallels = 1,
        TimeSpan throttle = default)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(parallels, 1, nameof(parallels));
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1, nameof(limit));

        if (delay == default) delay = TimeSpan.FromSeconds(12);

        if (throttle == default) throttle = TimeSpan.FromSeconds(1);

        var waiter = new SemaphoreSlim(parallels);
        var waiters = 0;

        var last = DateTime.MinValue;

        return builder.HandleAsync((context, cancellationToken) =>
        {
            ++waiters;

            if (last == DateTime.MinValue && waiters >= limit)
            {
                last = DateTime.UtcNow.Add(delay);

                return context
                    .ToMessageController()
                    .PublishMessageAsync(message => message
                        .SetContent(content => content
                            .AddText($"Too many requests, please wait a {delay.TotalSeconds} seconds.",
                                MonospaceTextStyle.FromTextRange)),
                        cancellationToken)
                    .ContinueWith(_ =>
                    {
                        --waiters;
                    }, TaskContinuationOptions.RunContinuationsAsynchronously)
                    .AsValueTask();
            }

            waiter.Wait(cancellationToken);

            if (last != DateTime.MinValue)
            {
                if (waiters <= 1 && DateTime.UtcNow >= last)
                {
                    last = DateTime.MinValue;
                }
                else
                {
                    --waiters;
                    waiter.Release();

                    return ValueTask.CompletedTask;
                }
            }

            return handler(context, cancellationToken).AsTask().ContinueWith(_ =>
                {
                    Thread.Sleep(throttle);

                    --waiters;
                    waiter.Release();
                }, TaskContinuationOptions.RunContinuationsAsynchronously)
                .AsValueTask();
        });
    }
}
