using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class TakeWhileSignalInterceptor<T>(Func<T, CancellationToken, bool> @while)
    : SignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private bool _completed;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        if (_completed) return InterceptionResult.Break();

        lock (_locker)
        {
            if (@while(signal, cancellationToken)) return InterceptionResult.Continue();

            _completed = true;
        }

        return InterceptionResult.Break();
    }
}
