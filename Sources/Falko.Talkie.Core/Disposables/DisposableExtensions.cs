namespace Falko.Talkie.Disposables;

public static partial class DisposableExtensions
{
    public static T DisposeWith<T>
    (
        this T disposable,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IDisposable
    {
        enumerableDisposables.Register(disposable);
        return disposable;
    }

    public static T DisposeAsyncWith<T>
    (
        this T asyncDisposable,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IAsyncDisposable
    {
        enumerableDisposables.Register(asyncDisposable);
        return asyncDisposable;
    }

    public static async ValueTask<T> DisposeWith<T>
    (
        this ValueTask<T> asyncDisposableValueTask,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Register(asyncDisposable);
        return asyncDisposable;
    }

    public static async Task<T> DisposeWith<T>
    (
        this Task<T> asyncDisposableValueTask,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Register(asyncDisposable);
        return asyncDisposable;
    }

    public static async ValueTask<T> DisposeAsyncWith<T>
    (
        this ValueTask<T> asyncDisposableValueTask,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IAsyncDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Register(asyncDisposable);
        return asyncDisposable;
    }

    public static async Task<T> DisposeAsyncWith<T>
    (
        this Task<T> asyncDisposableValueTask,
        IRegisterOnlyDisposableScope enumerableDisposables
    ) where T : IAsyncDisposable
    {
        var asyncDisposable = await asyncDisposableValueTask;
        enumerableDisposables.Register(asyncDisposable);
        return asyncDisposable;
    }
}
