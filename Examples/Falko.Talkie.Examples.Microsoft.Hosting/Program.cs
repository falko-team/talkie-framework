using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Talkie.Examples;
using Talkie.Hosting;

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
        .AddIntegrations<TelegramSubscriber>()
        .AddBehaviors<HelloSubscriber>()
        .AddBehaviors<StartSubscriber>())
    .RunConsoleAsync();
