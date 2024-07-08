using Talkie.Collections;

namespace Talkie.Disposables;

public static partial class DisposableExtensions
{
    public static T DisposeWith<T>(this T disposable, IEnumerableDisposable enumerableDisposables) where T : IDisposable
    {
        enumerableDisposables.Add(disposable);
        return disposable;
    }

    public static T DisposeAsyncWith<T>(this T asyncDisposable, IEnumerableDisposable enumerableDisposables) where T : IAsyncDisposable
    {
        enumerableDisposables.Add(asyncDisposable);
        return asyncDisposable;
    }

    public static async ValueTask<T> DisposeAsyncWith<T>(this ValueTask<T> asyncDisposableValueTask, IEnumerableDisposable enumerableDisposables) where T : IAsyncDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Add(asyncDisposable);
        return asyncDisposable;
    }

    public static async Task<T> DisposeAsyncWith<T>(this Task<T> asyncDisposableValueTask, IEnumerableDisposable enumerableDisposables) where T : IAsyncDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Add(asyncDisposable);
        return asyncDisposable;
    }
}
