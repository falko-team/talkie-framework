namespace Talkie.Validations;

public readonly ref struct Throwable<T>(T value, string? name = null)
{
    public readonly T Value = value;

    public readonly string? Name = name;
}
