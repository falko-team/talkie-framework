using Talkie.Flows;

namespace Talkie.Connections;

public abstract class SignalConnection : ISignalConnection
{
    private readonly SemaphoreSlim _locker = new(1, 1);

    private readonly CancellationTokenSource _cancellation = new();

    private volatile bool _initialized;

    private volatile bool _disposed;

    protected SignalConnection(ISignalFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);

        Flow = flow;
    }

    public ISignalFlow Flow { get; }

    public bool IsInitialized => _initialized;

    public Task? Executing { get; private set; }

    public bool IsDisposed => _disposed;

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_initialized) throw new InvalidOperationException(nameof(IsInitialized));

        try
        {
            await _locker.WaitAsync(cancellationToken);

            await WhenInitializingAsync(cancellationToken);

            Executing = Task.Factory.StartNew(() => WhenExecutingAsync(_cancellation.Token),
                _cancellation.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            _initialized = true;
        }
        finally
        {
            _locker.Release();
        }
    }

    protected abstract Task WhenInitializingAsync(CancellationToken cancellationToken);

    protected abstract Task WhenExecutingAsync(CancellationToken cancellationToken);

    protected abstract Task WhenDisposingAsync();

    public async ValueTask DisposeAsync()
    {
        if (_initialized is false || _disposed) return;

        try
        {
            await _locker.WaitAsync();

            await _cancellation.CancelAsync();

            _cancellation.Dispose();

            if (Executing is not null)
            {
                await Executing;

                Executing.Dispose();
            }

            await WhenDisposingAsync();

            _disposed = true;
        }
        finally
        {
            _locker.Release();
        }
    }
}
