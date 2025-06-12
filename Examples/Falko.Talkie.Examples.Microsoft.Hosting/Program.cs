using Falko.Talkie.Examples;
using Falko.Talkie.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

// Please set 'Telegram:Token' environment variable before running 💚💚💚

await new HostBuilder()
    .ConfigureAppConfiguration(configuration => configuration
        .AddEnvironmentVariables())
    .UseSerilog((_, configuration) => configuration
        .MinimumLevel.Verbose()
        .WriteTo.Console())
    .UseTalkie(configuration => configuration
        .SetSignalsLogging())
    .ConfigureServices(services => services
        .AddIntegrationsSubscriber<TelegramSubscriber>()
        .AddBehaviorsSubscriber<HelloSubscriber>()
        .AddBehaviorsSubscriber<StartSubscriber>())
    .RunConsoleAsync();
