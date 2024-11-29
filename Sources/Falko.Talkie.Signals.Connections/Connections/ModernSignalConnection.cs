namespace Talkie.Connections;

public abstract class ModernSignalConnection : ISignalConnection
{
    private readonly SemaphoreSlim _locker = new(1, 1);

    public bool IsInitialized { get; private set; }

    public bool IsDisposed { get; private set; }

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfInitialized();

        try
        {
            await _locker.WaitAsync(cancellationToken);

            ThrowIfInitialized();

            await InitializeCoreAsync(cancellationToken);

            IsInitialized = true;
        }
        finally
        {
            _locker.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (IsDisposedOrUninitialized()) return;

        try
        {
            await _locker.WaitAsync();

            if (IsDisposedOrUninitialized()) return;

            await DisposeCoreAsync();

            IsDisposed = true;
        }
        finally
        {
            _locker.Release();
        }
    }

    protected abstract ValueTask InitializeCoreAsync(CancellationToken cancellationToken);

    protected abstract ValueTask DisposeCoreAsync();

    private void ThrowIfInitialized()
    {
        if (IsInitialized) throw new InvalidOperationException("The connection is already initialized.");
    }

    private bool IsDisposedOrUninitialized()
    {
        return IsDisposed || IsInitialized is false;
    }
}
