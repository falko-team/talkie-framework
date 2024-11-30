using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Talkie.Subscribers;

namespace Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IServiceCollection AddIntegrationsSubscriber<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>
    (
        this IServiceCollection services
    ) where T : class, IIntegrationsSubscriber
    {
        return services.AddSingleton<IIntegrationsSubscriber, T>();
    }
}
