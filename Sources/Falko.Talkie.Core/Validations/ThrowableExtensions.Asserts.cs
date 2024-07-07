using System.Runtime.CompilerServices;

namespace Talkie.Validations;

public static partial class ThrowableExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Null<T>(this Throwable<T> throwable)
    {
        if (throwable.Value is null)
        {
            throw new ArgumentNullException(throwable.Name);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Disposed<T>(this Throwable<bool> throwable)
    {
        if (throwable.Value)
        {
            throw new ObjectDisposedException(nameof(T));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NullOrEmpty(this Throwable<string> throwable)
    {
        if (string.IsNullOrEmpty(throwable.Value))
        {
            throw new ArgumentException("Value cannot be null or empty.", throwable.Name);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NullOrWhiteSpace(this Throwable<string> throwable)
    {
        if (string.IsNullOrWhiteSpace(throwable.Value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", throwable.Name);
        }
    }
}
