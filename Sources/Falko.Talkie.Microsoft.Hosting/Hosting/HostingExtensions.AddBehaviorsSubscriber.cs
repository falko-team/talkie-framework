using System.Diagnostics.CodeAnalysis;
using Falko.Talkie.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace Falko.Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IServiceCollection AddBehaviorsSubscriber<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>
    (
        this IServiceCollection services
    ) where T : class, IBehaviorsSubscriber
    {
        return services.AddSingleton<IBehaviorsSubscriber, T>();
    }
}
