using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Talkie.Subscribers;

namespace Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IHostBuilder AddIntegrations<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IHostBuilder builder)
        where T : class, IIntegrationsSubscriber
    {
        return builder.ConfigureServices(services => services
            .AddIntegrations<T>());
    }

    public static IServiceCollection AddIntegrations<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IServiceCollection services)
        where T : class, IIntegrationsSubscriber
    {
        return services.AddSingleton<IIntegrationsSubscriber, T>();
    }
}
