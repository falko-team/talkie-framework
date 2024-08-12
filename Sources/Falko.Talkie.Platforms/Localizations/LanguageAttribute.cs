namespace Talkie.Localizations;

[AttributeUsage(AttributeTargets.Field)]
public sealed class LanguageAttribute(Language language) : Attribute
{
    public readonly Language Language = language;

    public override string ToString() => Language.ToString();

    public override int GetHashCode() => Language.GetHashCode();

    public override bool Equals(object? obj) => obj is LanguageAttribute other && Language == other.Language;
}
