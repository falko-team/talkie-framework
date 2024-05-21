namespace Falko.Talkie.Disposables;

public sealed class DelegatedDisposable(Action dispose) : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        dispose();

        _disposed = true;
    }
}
