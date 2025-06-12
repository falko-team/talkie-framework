using System.Numerics;
using System.Runtime.CompilerServices;

namespace Falko.Talkie.Validation;

public static partial class Assert
{
    public static class ArgumentOutOfRangeException
    {
        public static T ThrowIfEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IEquatable<T>?
        {
            System.ArgumentOutOfRangeException.ThrowIfEqual(value, other, paramName);

            return value;
        }

        public static T ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IEquatable<T>?
        {
            System.ArgumentOutOfRangeException.ThrowIfNotEqual(value, other, paramName);

            return value;
        }

        public static T ThrowIfGreaterThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other, paramName);

            return value;
        }

        public static T ThrowIfGreaterThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, other, paramName);

            return value;
        }

        public static T ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfLessThan(value, other, paramName);

            return value;
        }

        public static T ThrowIfLessThanOrEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, other, paramName);

            return value;
        }

        public static T ThrowIfZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfZero(value, paramName);

            return value;
        }

        public static T ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);

            return value;
        }

        public static T ThrowIfNegativeOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, paramName);

            return value;
        }

        public static T ThrowIfPositive<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            if (T.IsPositive(value))
            {
                throw new System.ArgumentOutOfRangeException(paramName, value, "Value must be positive.");
            }

            return value;
        }

        public static T ThrowIfPositiveOrZero<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            if (T.IsZero(value) || T.IsPositive(value))
            {
                throw new System.ArgumentOutOfRangeException(paramName, value, "Value must be positive or zero.");
            }

            return value;
        }
    }
}
