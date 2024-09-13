using Talkie.Sequences;

namespace Talkie.Disposables;

public sealed class ForwardDisposableScope : ElementaryDisposableScope
{
    protected override async ValueTask DisposeAsync(Sequence<IAsyncDisposable> asyncDisposables)
    {
        var exceptions = new Sequence<Exception>();

        foreach (var asyncDisposable in asyncDisposables)
        {
            try
            {
                await asyncDisposable.DisposeAsync();
            }
            catch (Exception exception)
            {
                exceptions.Add(exception);
            }
        }

        if (exceptions.Count > 0)
        {
            throw new AggregateException(exceptions);
        }
    }
}
