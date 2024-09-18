using System.Collections.Concurrent;
using System.Reflection;

namespace Talkie.Localizations;

public static class LanguageCodes
{
    private static readonly ConcurrentDictionary<Language, LanguageCode> LanguageCodesByLanguage = new();

    private static readonly ConcurrentDictionary<string, LanguageCode> LanguageCodesByLanguageCodeName = new();

    public static bool TryGetLanguageCode(this Language language, out LanguageCode languageCode)
    {
        languageCode = LanguageCode.Unknown;

        if (language is Language.Unknown) return false;

        if (LanguageCodesByLanguage.TryGetValue(language, out languageCode)) return true;

        var field = typeof(Language).GetField(language.ToString());

        if (field?.GetCustomAttribute<LanguageCodeAttribute>() is not { } attribute) return false;

        languageCode = attribute.LanguageCode;

        LanguageCodesByLanguage.TryAdd(language, languageCode);

        return true;
    }

    public static bool TryGetLanguageCode(string languageCodeName, out LanguageCode languageCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(languageCodeName);

        if (LanguageCodesByLanguageCodeName.TryGetValue(languageCodeName, out languageCode)) return true;

        if (Enum.TryParse(languageCodeName, true, out languageCode) is false) return false;

        LanguageCodesByLanguageCodeName.TryAdd(languageCodeName, languageCode);

        return true;
    }
}
