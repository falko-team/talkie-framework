using System.Runtime.CompilerServices;

namespace Talkie.Validations;

public static class ThrowableExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Throwable<T> ThrowIf<T>(this T value, [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        return new Throwable<T>(value, name);
    }

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
    public static void NullOrWhiteSpace(this Throwable<string> throwable)
    {
        if (string.IsNullOrWhiteSpace(throwable.Value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", throwable.Name);
        }
    }
}
