namespace Talkie.Localizations;

[AttributeUsage(AttributeTargets.Field)]
public sealed class LanguageCodeAttribute(LanguageCode languageCode) : Attribute
{
    public readonly LanguageCode LanguageCode = languageCode;

    public override string ToString() => LanguageCode.ToString();

    public override int GetHashCode() => LanguageCode.GetHashCode();

    public override bool Equals(object? obj) => obj is LanguageCodeAttribute other && LanguageCode == other.LanguageCode;
}
