using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Talkie.Subscribers;

namespace Talkie.Hosting;

public static partial class HostingExtensions
{
    public static IServiceCollection AddBehaviors<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>
    (
        this IServiceCollection services
    ) where T : class, IBehaviorsSubscriber
    {
        return services.AddSingleton<IBehaviorsSubscriber, T>();
    }
}
