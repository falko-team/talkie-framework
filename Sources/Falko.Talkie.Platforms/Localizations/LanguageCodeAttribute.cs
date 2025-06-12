namespace Falko.Talkie.Localizations;

[AttributeUsage(AttributeTargets.Field)]
public sealed class LanguageCodeAttribute(string languageCode) : Attribute
{
    public readonly string LanguageCode = languageCode;

    public override string ToString() => LanguageCode;

    public override int GetHashCode() => LanguageCode.GetHashCode();

    public override bool Equals(object? obj) => obj is LanguageCodeAttribute other && LanguageCode == other.LanguageCode;
}
