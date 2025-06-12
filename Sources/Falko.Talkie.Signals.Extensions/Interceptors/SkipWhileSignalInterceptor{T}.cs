using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

internal sealed class SkipWhileSignalInterceptor<T>(Func<T, CancellationToken, bool> @while)
    : SignalInterceptor<T> where T : Signal
{
    private readonly Lock _locker = new();

    private bool _completed;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        if (_completed) return InterceptionResult.Continue();

        lock (_locker)
        {
            if (@while(signal, cancellationToken)) return InterceptionResult.Break();

            _completed = true;
        }

        return InterceptionResult.Continue();
    }
}
