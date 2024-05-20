using Falko.Unibot.Models.Messages;
using Falko.Unibot.Platforms;

namespace Falko.Unibot.Validations;

public static class ThrowableExtensions
{
    public static void NotPlatform<T>(this Throwable<IIncomingMessage> throwable) where T : class, IPlatform
    {
        if (throwable.Value.Platform is not T)
        {
            throw new PlatformNotSupportedException($"Supports only {typeof(T).Name} platform.");
        }
    }
}
