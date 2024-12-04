using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class TakeWhileSignalInterceptor(Func<Signal, CancellationToken, bool> @while) : ISignalInterceptor
{
    private readonly Lock _locker = new();

    private volatile bool _completed;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
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
