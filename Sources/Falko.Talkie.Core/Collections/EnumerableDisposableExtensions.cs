using Talkie.Disposables;

namespace Talkie.Collections;

public static partial class EnumerableDisposableExtensions
{
    public static void Add(this IEnumerableDisposable enumerableDisposables, Action dispose)
    {
        enumerableDisposables.Add(new DelegatedDisposable(dispose));
    }
}
