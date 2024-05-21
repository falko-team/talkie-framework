using System.Collections;
using Falko.Talkie.Validations;

namespace Falko.Talkie.Collections;

public sealed class DisposableQueue : IEnumerableDisposable
{
    private readonly Queue _disposables = new();

    public void Enqueue(IDisposable disposable)
    {
        disposable.ThrowIf().Null();

        _disposables.Enqueue(disposable);
    }

    void IEnumerableDisposable.Add(IDisposable disposable) => Enqueue(disposable);

    public void Enqueue(IAsyncDisposable asyncDisposable)
    {
        asyncDisposable.ThrowIf().Null();

        _disposables.Enqueue(asyncDisposable);
    }

    void IEnumerableDisposable.Add(IAsyncDisposable asyncDisposable) => Enqueue(asyncDisposable);

    IEnumerator<IAsyncDisposable> IEnumerable<IAsyncDisposable>.GetEnumerator()
    {
        return _disposables.OfType<IAsyncDisposable>().GetEnumerator();
    }

    IEnumerator<IDisposable> IEnumerable<IDisposable>.GetEnumerator()
    {
        return _disposables.OfType<IDisposable>().GetEnumerator();
    }

    public IEnumerator GetEnumerator() => _disposables.GetEnumerator();

    public void Dispose()
    {
        while (_disposables.Dequeue() is var disposable && disposable is not null)
        {
            switch (disposable)
            {
                case IDisposable disposableValue:
                    disposableValue.Dispose();
                    break;
                case IAsyncDisposable asyncDisposableValue:
                    asyncDisposableValue.DisposeAsync().AsTask().Wait();
                    break;
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        while (_disposables.Count > 0)
        {
            var disposable = _disposables.Dequeue();

            switch (disposable)
            {
                case IAsyncDisposable asyncDisposableValue:
                    await asyncDisposableValue.DisposeAsync();
                    break;
                case IDisposable disposableValue:
                    disposableValue.Dispose();
                    break;
            }
        }
    }

    ~DisposableQueue() => Dispose();
}
