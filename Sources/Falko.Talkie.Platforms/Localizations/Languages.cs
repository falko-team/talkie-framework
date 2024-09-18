using System.Collections.Concurrent;
using System.Reflection;

namespace Talkie.Localizations;

public static class Languages
{
    private static readonly ConcurrentDictionary<LanguageCode, Language> LanguagesByLanguageCode = new();

    private static readonly ConcurrentDictionary<string, Language> LanguagesByLanguageName = new();

    public static bool TryGetLanguage(this LanguageCode languageCode, out Language language)
    {
        language = Language.Unknown;

        if (languageCode is LanguageCode.Unknown) return false;

        if (LanguagesByLanguageCode.TryGetValue(languageCode, out language)) return true;

        var field = typeof(LanguageCode).GetField(languageCode.ToString());

        if (field?.GetCustomAttribute<LanguageAttribute>() is not { } attribute) return false;

        language = attribute.Language;

        LanguagesByLanguageCode.TryAdd(languageCode, language);

        return true;
    }

    public static bool TryGetLanguage(string languageName, out Language language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(languageName);

        if (LanguagesByLanguageName.TryGetValue(languageName, out language)) return true;

        if (Enum.TryParse(languageName, true, out language) is false) return false;

        LanguagesByLanguageName.TryAdd(languageName, language);

        return true;
    }
}
