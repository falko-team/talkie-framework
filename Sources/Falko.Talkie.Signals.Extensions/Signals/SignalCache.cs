namespace Falko.Talkie.Signals;

public static class SignalCache<T> where T : Signal, new()
{
    public static readonly T Instance = new();
}
