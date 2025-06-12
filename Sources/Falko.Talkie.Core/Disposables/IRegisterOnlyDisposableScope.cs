namespace Falko.Talkie.Disposables;

public interface IRegisterOnlyDisposableScope
{
    void Register(IAsyncDisposable asyncDisposable);
}
