using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class SkipWhileSignalInterceptor(Func<Signal, CancellationToken, bool> @while) : ISignalInterceptor
{
    private readonly object _locker = new();

    private bool _done;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            if (_done) return InterceptionResult.Continue();

            if (@while(signal, cancellationToken)) return InterceptionResult.Break();

            _done = true;

            return InterceptionResult.Continue();
        }
    }
}
