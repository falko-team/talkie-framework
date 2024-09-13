using Talkie.Concurrent;
using Talkie.Sequences;

namespace Talkie.Disposables;

public sealed class ParallelDisposableScope : ElementaryDisposableScope
{
    protected override async ValueTask DisposeAsync(Sequence<IAsyncDisposable> asyncDisposables)
    {
        await asyncDisposables
            .ToFrozenSequence()
            .Parallelize()
            .ForEachAsync((asyncDisposable, _) => asyncDisposable
                .DisposeAsync());
    }
}
