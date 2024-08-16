using Talkie.Flows;

namespace Talkie.Connections;

/// <summary>
/// Represents a connection to a signal flow.
/// </summary>
public interface ISignalConnection : IAsyncDisposable
{
    /// <summary>
    /// Gets the <see cref="ISignalFlow"/> that the connection is connected to.
    /// </summary>
    ISignalFlow Flow { get; }

    /// <summary>
    /// Gets the connection initialization status.
    /// </summary>
    bool Initialized { get; }

    /// <summary>
    /// Gets the connection execution task.
    /// </summary>
    Task? Executing { get; }

    /// <summary>
    /// Gets the connection disposal status.
    /// </summary>
    bool Disposed { get; }

    /// <summary>
    /// Initializes the connection.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
    /// <exception cref="InvalidOperationException">The connection is already initialized.</exception>
    ValueTask InitializeAsync(CancellationToken cancellationToken = default);
}
