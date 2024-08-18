using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Talkie.Validations;

public static partial class ThrowableExtensions
{
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Throwable<T> ThrowIf<T>([NotNull] this T value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
    {
        return new Throwable<T>(value, name);
    }
}
