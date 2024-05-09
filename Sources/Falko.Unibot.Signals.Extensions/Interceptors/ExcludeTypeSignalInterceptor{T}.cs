using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public sealed class ExcludeTypeSignalInterceptor<TTOriginal, TChecked> : SignalInterceptor<TTOriginal>
    where TTOriginal : Signal
    where TChecked : TTOriginal
{
    public static readonly ExcludeTypeSignalInterceptor<TTOriginal, TChecked> Instance = new();

    private ExcludeTypeSignalInterceptor() { }

    public override InterceptionResult Intercept(TTOriginal signal, CancellationToken cancellationToken)
    {
        return signal is not TChecked;
    }
}
