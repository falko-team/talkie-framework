using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Talkie.Subscribers;

namespace Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IHostBuilder AddBehaviors<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IHostBuilder builder)
        where T : class, IBehaviorsSubscriber
    {
        return builder.ConfigureServices(services => services
            .AddBehaviors<T>());
    }

    public static IServiceCollection AddBehaviors<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IServiceCollection services)
        where T : class, IBehaviorsSubscriber
    {
        return services.AddSingleton<IBehaviorsSubscriber, T>();
    }
}
