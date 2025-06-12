using Falko.Talkie.Flows;

namespace Falko.Talkie.Signals;

/// <summary>
/// Represents a <see cref="Signal"/> that can publish on a <see cref="ISignalFlow"/>.
/// </summary>
/// <remarks>
/// A <see cref="Signal"/> is should be immutable
/// and be used to represent a context
/// that can be published on a <see cref="ISignalFlow"/>.
/// </remarks>
/// <example lang="csharp">
/// var flow = new SignalFlow();
/// var signal = new MySignal();
/// await flow.PublishAsync(signal);
/// </example>
public abstract record Signal;
