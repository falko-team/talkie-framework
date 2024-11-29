using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Reflection;

namespace Talkie.Localizations;

public static class Languages
{
    private static readonly ConcurrentDictionary<string, Language> LanguagesByLanguageName = new();

    private static readonly FrozenDictionary<string, Language> LanguagesByLanguageCodeName;

    static Languages()
    {
        var fields = typeof(Language).GetFields();

        var languages = new KeyValuePair<string, Language>[fields.Length];

        var iterator = 0;

        foreach (var context in fields)
        {
            if (context.GetCustomAttribute<LanguageCodeAttribute>() is not { } attribute) continue;

            if (context.GetValue(null) is not Language language) continue;

            languages[iterator] = new KeyValuePair<string, Language>(attribute.LanguageCode, language);

            ++iterator;
        }

        if (iterator != languages.Length) languages = languages[..iterator];

        LanguagesByLanguageCodeName = languages.ToFrozenDictionary();
    }

    public static bool TryGetFromLanguageName(string name, out Language language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (LanguagesByLanguageName.TryGetValue(name, out language)) return true;

        if (Enum.TryParse(name, true, out language) is false) return false;

        LanguagesByLanguageName.TryAdd(name, language);

        return true;
    }

    public static bool TryGetFromLanguageCodeName(string name, out Language language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return LanguagesByLanguageCodeName.TryGetValue(name, out language);
    }
}
