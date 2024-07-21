using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class SkipWhileSignalInterceptor<T>(Func<T, CancellationToken, bool> @while) : SignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private bool _done;

    public override InterceptionResult Intercept(T signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            if (_done) return InterceptionResult.Continue();

            if (@while(signal,cancellationToken)) return InterceptionResult.Break();

            _done = true;

            return InterceptionResult.Continue();
        }
    }
}
