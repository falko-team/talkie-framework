using System.Runtime.CompilerServices;

namespace Falko.Talkie.Common;

public static partial class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);
}
