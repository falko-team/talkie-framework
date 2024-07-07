namespace Talkie.Bridges.Telegram.Configurations;

public sealed record ClientConfiguration
{
    public HttpProtocol ProtocolVersion { get; init; } = HttpProtocol.Version20;

    public bool UseGzipDecompression { get; init; } = true;

    public bool UseGzipCompression { get; init; } = true;

    public int ConnectionsPoolMaxSize { get; init; } = 100;

    public TimeSpan PooledConnectionIdleTimeout { get; init; } = TimeSpan.FromSeconds(30);

    public TimeSpan PooledConnectionLifetime { get; init; } = TimeSpan.FromMinutes(1);

    public TimeSpan ConnectTimeout { get; init; } = TimeSpan.FromSeconds(30);

    public TimeSpan KeepAlivePingDelay { get; init; } = TimeSpan.FromSeconds(60);

    public TimeSpan KeepAlivePingTimeout { get; init; } = TimeSpan.FromSeconds(15);
}
