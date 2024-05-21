namespace Falko.Talkie.Disposables;

public sealed class SerialDisposable : IDisposable, IAsyncDisposable
{
    private object? _disposable;

    public void Dispose(IDisposable disposable)
    {
        Dispose();

        _disposable = disposable;
    }

    public async ValueTask DisposeAsync(IAsyncDisposable disposable)
    {
        await DisposeAsync();

        _disposable = disposable;
    }

    public void Dispose()
    {
        if (_disposable is null)
        {
            return;
        }

        switch (_disposable)
        {
            case IDisposable disposable:
                disposable.Dispose();
                break;
            case IAsyncDisposable disposable:
                disposable.DisposeAsync().AsTask().Wait();
                break;
        }

        _disposable = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposable is null)
        {
            return;
        }

        switch (_disposable)
        {
            case IAsyncDisposable disposable:
                await disposable.DisposeAsync();
                break;
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }

        _disposable = null;
    }
}
