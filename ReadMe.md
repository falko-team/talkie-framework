# <img src="Icon64.png" width="24" hspace="5" /> Talkie - Build Bots Once, Deploy Everywhere

> Talkie is under active development. The underlying C# framework libraries is subject to change.

Talkie is a framework for creating bots across messaging platforms. Write code once, deploy everywere (for Telegram, Discord, and more). Also, its lightweight and modular design, parallel processing and native AOT compilation, keep your bots blazing fast with minimal memory usage.

## <img src="Icon64.png" width="18" hspace="5" /> Features

- **Fast and Lightweight**: Talkie is designed to be fast and lightweight, making it ideal for use in resource-constrained environments or high-traffic, large-scale bots.
- **Native-AOT Support (Latest .NET 9)**: Talkie provides native Ahead-of-Time (AOT) compilation support with the latest .NET 9 framework, enabling developers to compile bots to native code for enhanced performance and memory efficiency.
- **Parallel Processing**: Talkie is optimized to leverage multi-core processors, empowering developers to create bots capable of handling multiple messages concurrently.
- **OOP and Functional Programming**: Talkie offers flexibility by supporting both object-oriented and functional programming paradigms, allowing developers to choose the style that best aligns with their preferences and project requirements.
- **Self-Contained and Independent**: Talkie is self-contained and does not rely on external libraries. This eliminates compatibility concerns, reduces the overall footprint, and simplifies deployment.
- **Cross-Platform Compatibility**: Talkie is engineered for cross-platform compatibility, enabling developers to write code once and deploy it seamlessly across diverse bot platforms, including Telegram, Discord, and more.
- **Extensible Architecture**: Talkie is designed with extensibility in mind, making it easy for developers to add new features, functionalities, and expand platform support as needed.
- **Comprehensive Functionality**: Talkie offers a wide array of features, and managing complex branched code for different bot platforms, all while simplifying code that would be more cumbersome on other platforms.

## <img src="Icon64.png" width="18" hspace="5" /> Usage

### <img src="Icon64.png" width="14" hspace="5" /> Install the Talkie Platforms Package:

Install Microsoft Hosting infrastructure:

```bash
dotnet add package Falko.Talkie.Microsoft.Hosting
```

Install the Talkie Telegram Platform package, If you're using Telegram:

```bash
dotnet add package Falko.Talkie.Platforms.Telegram
```

### <img src="Icon64.png" width="14" hspace="5" /> Explore the Examples:

To get started quickly, check out the [Examples](Examples) folder in the Talkie repository
for illustrative code samples and usage demonstrations.

Or explore [Simple Wallet Bot (Rider Coin Bot)](https://github.com/falko-team/rider-coin)!

Or watch simple example of code:

```C#
await new HostBuilder()
    .UseTalkie(configuration => configuration)
    .ConfigureServices(services => services
        .AddIntegrationSubscriber<TelegramSubscriber>()
        .AddBehaviorsSubscriber<HelloWorldSubscriber>())
    .RunConsoleAsync();

file sealed class HelloWorldSubscriber : IBehaviorsSubscriber
{
    public void Subscribe
    (
        ISignalFlow flow,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken
    )
    {
        flow.Subscribe<MessagePublishedSignal>(signals => signals
            .SkipSelfRelated()
            .SkipOlderThan(TimeSpan.FromMinutes(1))
            .Where(signal => signal
                .Message
                .GetText()
                .StartsWith("/hello", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync(context => context
                .ToMessageController()
                .PublishMessageAsync("Hi!", cancellationToken)
                .AsValueTask()))
            .UnsubscribeWith(disposables);
    }
}

file sealed class TelegramSubscriber : IIntegrationsSubscriber
{
    public async Task SubscribeAsync
    (
        ISignalFlow flow,
        IRegisterOnlyDisposableScope disposables,
        CancellationToken cancellationToken
    )
    {
        await flow.ConnectTelegramAsync("MY_TOKEN", cancellationToken)
            .DisposeAsyncWith(disposables);
    }
}
```

## <img src="Icon64.png" width="18" hspace="5" /> License

This project is licensed under the [BSD 2-Clause License](License.md).

_Contributions are welcome!_

**© 2024, Falko Team**
