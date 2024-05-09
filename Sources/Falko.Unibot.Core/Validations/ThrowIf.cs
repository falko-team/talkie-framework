using System.Runtime.CompilerServices;

namespace Falko.Unibot.Validations;

public static class ThrowIf
{
    public static void Null<T>(T? argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
        where T : notnull
    {
        if (argument is null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void Disposed(bool disposed, string? objectName = null)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(objectName);
        }
    }

    public static void Disposed<T>(bool disposed)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(T));
        }
    }

    public static void LessThan<T>(T value, T limit, [CallerArgumentExpression(nameof(value))] string? argumentName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(limit) >= 0)
        {
            throw new ArgumentOutOfRangeException(argumentName, value, $"Value must be less than {limit}");
        }
    }

    public static void GreaterThan<T>(T value, T limit, [CallerArgumentExpression(nameof(value))] string? argumentName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(limit) <= 0)
        {
            throw new ArgumentOutOfRangeException(argumentName, value, $"Value must be greater than {limit}");
        }
    }
}
