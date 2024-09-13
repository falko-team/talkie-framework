namespace Talkie.Disposables;

public interface IRegisterOnlyDisposableScope
{
    void Register(IAsyncDisposable asyncDisposable);
}
