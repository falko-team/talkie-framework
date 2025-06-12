namespace Falko.Talkie.Hosting;

public sealed class TalkieHostingConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether the host should shutdown when an unobserved exception is thrown.
    /// </summary>
    public bool ShutdownOnUnobservedExceptions { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether signals should be logged.
    /// </summary>
    public bool LogSignals { get; set; }
}
