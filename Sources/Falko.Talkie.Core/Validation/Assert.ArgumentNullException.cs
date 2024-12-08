using System.Runtime.CompilerServices;

namespace Talkie.Validation;

// ReSharper disable RedundantNameQualifier
public static partial class Assert
{
    public static class ArgumentNullException
    {
        public static T ThrowIfNull<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            System.ArgumentNullException.ThrowIfNull(value, paramName);

            return value;
        }

        public static string ThrowIfNullOrWhiteSpace(string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            System.ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);

            return value;
        }

        public static string ThrowIfNullOrEmpty(string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            System.ArgumentException.ThrowIfNullOrEmpty(value, paramName);

            return value;
        }
    }
}
