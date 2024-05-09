using Falko.Unibot.Disposables;

namespace Falko.Unibot.Collections;

public static class DisposableSequenceExtensions
{
    public static void Add(this IEnumerableDisposable enumerableDisposables, Action dispose)
    {
        enumerableDisposables.Add(new DelegatedDisposable(dispose));
    }
}
