using Talkie.Concurrent;
using Talkie.Exceptions;
using Talkie.Pipelines.Handling;
using Talkie.Sequences;
using Talkie.Signals;

namespace Talkie.Flows;

public sealed class SignalFlow : ISignalFlow
{
    private readonly Lock _locker = new();

    private readonly CancellationTokenSource _flowCancellationTokenSource = new();

    private readonly RemovableSequence<ISignalHandlingPipeline> _pipelines = new();

    private readonly ParallelismMeter _pipelinesParallelismMeter = new();

    private bool _disposed;

    public SignalFlow()
    {
        TaskScheduler.UnobservedTaskException += OnUnobservedSignalPublishingException;
    }

    public Subscription Subscribe(ISignalHandlingPipeline handlingPipeline)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        lock (_locker)
        {
            var remover = _pipelines.Add(handlingPipeline);

            return new Subscription(() =>
            {
                lock (_locker) remover.Remove();
            });
        }
    }

    public async Task PublishAsync(Signal signal, CancellationToken cancellationToken = default)
    {
        if (_disposed) return;

        using var publishCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_flowCancellationTokenSource.Token, cancellationToken);

        var publishCancellationToken = publishCancellationTokenSource.Token;

        try
        {
            // ReSharper disable once InconsistentlySynchronizedField
            await _pipelines.Parallelize(_pipelinesParallelismMeter)
                .ForEachAsync((pipeline, scopedCancellationToken) => pipeline.TransferAsync(this, signal, scopedCancellationToken),
                    cancellationToken: publishCancellationToken);
        }
        catch (SignalPublishingException exception)
        {
            if (exception.Flow == this)
            {
                throw;
            }

            throw new SignalPublishingException(this, exception);
        }
        catch (Exception exception)
        {
            throw new SignalPublishingException(this, exception);
        }
    }

    private void OnUnobservedSignalPublishingException(object? sender, UnobservedTaskExceptionEventArgs args)
    {
        var signalPublishingException = args
            .Exception
            .InnerExceptions
            .FirstOrDefault(exception => exception is SignalPublishingException) as SignalPublishingException;

        if (signalPublishingException is null) return;
        if (signalPublishingException.Flow != this) return;

        _ = signalPublishingException.Flow.PublishAsync(new UnobservedPublishingExceptionSignal(signalPublishingException));

        args.SetObserved();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _flowCancellationTokenSource.Cancel();
        _flowCancellationTokenSource.Dispose();

        TaskScheduler.UnobservedTaskException -= OnUnobservedSignalPublishingException;

        _disposed = true;
    }
}
