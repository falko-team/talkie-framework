using Talkie.Models.Messages;
using Talkie.Platforms;

namespace Talkie.Validations;

public static partial class ThrowableExtensions
{
    public static void NotPlatform<T>(this Throwable<IIncomingMessage> throwable) where T : class, IPlatform
    {
        if (throwable.Value.Platform is not T)
        {
            throw new PlatformNotSupportedException($"Supports only {typeof(T).Name} platform.");
        }
    }
}
