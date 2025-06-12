namespace Falko.Talkie.Connections;

/// <summary>
/// Represents a connection to a signal flow.
/// </summary>
public interface ISignalConnection : IAsyncDisposable
{
    /// <summary>
    /// Gets the connection initialization status.
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Gets the connection disposal status.
    /// </summary>
    bool IsDisposed { get; }

    /// <summary>
    /// Initializes the connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
    /// <exception cref="InvalidOperationException">The connection is already initialized.</exception>
    ValueTask InitializeAsync(CancellationToken cancellationToken = default);
}
