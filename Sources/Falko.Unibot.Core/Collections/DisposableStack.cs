using System.Collections;
using Falko.Unibot.Validations;

namespace Falko.Unibot.Collections;

public sealed class DisposableStack : IEnumerableDisposable
{
    private readonly Stack _disposables = new();

    public void Push(IDisposable disposable)
    {
        disposable.ThrowIf().Null();

        _disposables.Push(disposable);
    }

    void IEnumerableDisposable.Add(IDisposable disposable) => Push(disposable);

    public void Push(IAsyncDisposable asyncDisposable)
    {
        asyncDisposable.ThrowIf().Null();

        _disposables.Push(asyncDisposable);
    }

    void IEnumerableDisposable.Add(IAsyncDisposable asyncDisposable) => Push(asyncDisposable);

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
        while (_disposables.Pop() is var disposable && disposable is not null)
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
            var disposable = _disposables.Pop();

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

    ~DisposableStack() => Dispose();
}
