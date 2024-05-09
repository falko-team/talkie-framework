namespace Falko.Unibot.Disposables;

public sealed class EmptyDisposable : IDisposable
{
    public static readonly EmptyDisposable Instance = new();

    public void Dispose() { }
}
