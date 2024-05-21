namespace Falko.Talkie.Collections;

public interface IEnumerableDisposable : IEnumerable<IDisposable>, IEnumerable<IAsyncDisposable>,
    IDisposable, IAsyncDisposable
{
    void Add(IDisposable disposable);

    void Add(IAsyncDisposable asyncDisposable);
}
