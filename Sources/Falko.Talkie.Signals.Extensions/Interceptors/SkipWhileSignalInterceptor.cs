using Talkie.Signals;

namespace Talkie.Interceptors;

internal sealed class SkipWhileSignalInterceptor(Func<Signal, CancellationToken, bool> @while)
    : ISignalInterceptor
{
    private readonly Lock _locker = new();

    private bool _completed;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
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
