using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Talkie.Flows;
using Talkie.Subscribers;

namespace Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IHostBuilder UseTalkie
    (
        this IHostBuilder builder,
        TalkieHostingConfiguration configuration
    )
    {
        return builder
            .ConfigureServices(services => services
                .AddSingleton<TalkieHostingConfiguration>(_ => configuration)
                .AddSingleton<ISignalFlow, SignalFlow>()
                .AddHostedService<SubscribersService>()
                .TryAddShutdown(configuration)
                .TryAddSignalsLogging(configuration));
    }

    public static IHostBuilder UseTalkie(this IHostBuilder builder)
    {
        return builder.UseTalkie(new TalkieHostingConfiguration());
    }

    public static IHostBuilder UseTalkie
    (
        this IHostBuilder builder,
        TalkieHostingConfigurationBuilder configurationBuilder
    )
    {
        return builder.UseTalkie(configurationBuilder.Build());
    }

    public static IHostBuilder UseTalkie
    (
        this IHostBuilder builder,
        Func<TalkieHostingConfigurationBuilder, TalkieHostingConfigurationBuilder> configure
    )
    {
        return builder.UseTalkie(configure(new TalkieHostingConfigurationBuilder()));
    }

    private static IServiceCollection TryAddShutdown
    (
        this IServiceCollection services,
        TalkieHostingConfiguration configuration
    )
    {
        return configuration.ShutdownOnUnobservedExceptions
            ? services.AddBehaviorsSubscriber<UnobservedExceptionsShutdownSubscriber>()
            : services.AddBehaviorsSubscriber<UnobservedExceptionsLoggingSubscriber>();
    }

    private static IServiceCollection TryAddSignalsLogging
    (
        this IServiceCollection services,
        TalkieHostingConfiguration configuration
    )
    {
        return configuration.LogSignals
            ? services.AddBehaviorsSubscriber<SignalsLoggingSubscriber>()
            : services;
    }
}
