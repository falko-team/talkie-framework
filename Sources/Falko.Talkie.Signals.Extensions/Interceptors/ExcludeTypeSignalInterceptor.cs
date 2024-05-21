using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

public sealed class ExcludeTypeSignalInterceptor<T> : ISignalInterceptor where T : Signal
{
    public static readonly ExcludeTypeSignalInterceptor<T> Instance = new();

    private ExcludeTypeSignalInterceptor() { }

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return signal is not T;
    }
}
