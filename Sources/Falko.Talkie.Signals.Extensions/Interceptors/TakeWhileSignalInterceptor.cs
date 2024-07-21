using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class TakeWhileSignalInterceptor(Func<Signal, CancellationToken, bool> @while) : ISignalInterceptor
{
    private readonly object _locker = new();

    private bool _done;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            if (_done) return InterceptionResult.Break();

            if (@while(signal, cancellationToken)) return InterceptionResult.Continue();

            _done = true;

            return InterceptionResult.Break();
        }
    }
}
