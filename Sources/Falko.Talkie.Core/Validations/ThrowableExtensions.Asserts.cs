using System.Runtime.CompilerServices;

namespace Talkie.Validations;

public static partial class ThrowableExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Throwable<T> Null<T>(this in Throwable<T> throwable)
    {
        if (throwable.Value is null)
        {
            throw new ArgumentNullException(throwable.Name);
        }

        return ref throwable;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Throwable<bool> Disposed<T>(this Throwable<bool> throwable)
    {
        if (throwable.Value)
        {
            throw new ObjectDisposedException(nameof(T));
        }

        return throwable;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Throwable<string> NullOrEmpty(this Throwable<string> throwable)
    {
        if (string.IsNullOrEmpty(throwable.Value))
        {
            throw new ArgumentException("Value cannot be null or empty.", throwable.Name);
        }

        return throwable;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Throwable<string> NullOrWhiteSpace(this Throwable<string> throwable)
    {
        if (string.IsNullOrWhiteSpace(throwable.Value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", throwable.Name);
        }

        return throwable;
    }
}
