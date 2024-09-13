namespace Talkie.Hosting;

public sealed class TalkieHostingConfigurationBuilder
{
    private readonly TalkieHostingConfiguration _configuration = new();

    /// <summary>
    /// Sets a value indicating whether the host should shutdown when an unobserved exception is thrown.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The current instance of <see cref="TalkieHostingConfigurationBuilder"/>.</returns>
    public TalkieHostingConfigurationBuilder SetShutdownOnUnobservedExceptions(bool value = true)
    {
        _configuration.ShutdownOnUnobservedExceptions = value;
        return this;
    }

    /// <summary>
    /// Sets a value indicating whether signals should be logged.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The current instance of <see cref="TalkieHostingConfigurationBuilder"/>.</returns>
    public TalkieHostingConfigurationBuilder SetSignalsLogging(bool value = true)
    {
        _configuration.LogSignals = value;
        return this;
    }

    /// <summary>
    /// Builds the <see cref="TalkieHostingConfiguration"/>.
    /// </summary>
    /// <returns>The <see cref="TalkieHostingConfiguration"/>.</returns>
    public TalkieHostingConfiguration Build()
    {
        return _configuration;
    }
}
