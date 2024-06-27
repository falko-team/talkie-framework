using System.Diagnostics.CodeAnalysis;

namespace Talkie.Validations;

public readonly ref struct Throwable<T>([NotNull] T value, string? name = null)
{
    public readonly T Value = value;

    public readonly string? Name = name;
}
