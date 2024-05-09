using Falko.Unibot.Collections;
using Falko.Unibot.Concurrent;
using Falko.Unibot.Pipelines;
using Falko.Unibot.Signals;
using Falko.Unibot.Validations;

namespace Falko.Unibot.Flows;

public sealed class SignalFlow : ISignalFlow
{
    private readonly object _locker = new();

    private readonly CancellationTokenSource _flowCancellationTokenSource = new();

    private readonly RemovableSequence<ISignalPipeline> _pipelines = new();

    private readonly ParallelismMeter _pipelinesParallelismMeter = new();

    private bool _disposed;

    public Subscription Subscribe(ISignalPipeline pipeline)
    {
        ThrowIf.Disposed<SignalFlow>(_disposed);

        lock (_locker)
        {
            var remover = _pipelines.Add(pipeline);

            return new Subscription(() =>
            {
                lock (_locker) remover.Remove();
            });
        }
    }

    public Task PublishAsync(Signal signal, CancellationToken cancellationToken = default)
    {
        if (_disposed) return Task.CompletedTask;

        using var publishCancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(_flowCancellationTokenSource.Token, cancellationToken);

        var publishCancellationToken = publishCancellationTokenSource.Token;

        // ReSharper disable once InconsistentlySynchronizedField
        return _pipelines.Parallelize(_pipelinesParallelismMeter)
            .ForEachAsync((pipeline, scopedCancellationToken) => pipeline.Transfer(this, signal, scopedCancellationToken),
                cancellationToken: publishCancellationToken);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _flowCancellationTokenSource.Cancel();
        _flowCancellationTokenSource.Dispose();

        _disposed = true;
    }
}
