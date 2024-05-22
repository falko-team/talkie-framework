using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class IncludeTypeSignalInterceptor<TTOriginal, TChecked> : SignalInterceptor<TTOriginal>
    where TTOriginal : Signal
    where TChecked : TTOriginal
{
    public static readonly IncludeTypeSignalInterceptor<TTOriginal, TChecked> Instance = new();

    private IncludeTypeSignalInterceptor() { }

    public override InterceptionResult Intercept(TTOriginal signal, CancellationToken cancellationToken)
    {
        return signal is TChecked;
    }
}
