namespace Talkie.Connections;

public interface ISignalConnection : IAsyncDisposable
{
    ValueTask InitializeAsync(CancellationToken cancellationToken = default);
}
