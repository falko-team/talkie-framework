using Talkie.Mixins;
using Talkie.Platforms;

namespace Talkie.Connections;

/// <summary>
/// Represents a <see cref="ISignalConnection"/> mixin that provides a <see cref="IPlatform"/> property.
/// </summary>
public interface IWithPlatformSignalConnection : IWith<ISignalConnection>
{
    /// <summary>
    /// Gets the <see cref="IPlatform"/> that the connection is connected to.
    /// </summary>
    IPlatform? Platform { get; }
}
