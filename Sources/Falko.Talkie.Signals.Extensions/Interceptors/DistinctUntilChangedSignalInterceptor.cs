using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

public sealed class DistinctUntilChangedSignalInterceptor<TValue>(Func<Signal, CancellationToken, TValue> distinctUntilChanged)
    : ISignalInterceptor
{
    private readonly object _locker = new();

    private bool _isInterceptedBefore;

    private TValue _last = default!;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        lock (_locker)
        {
            var current = distinctUntilChanged(signal, cancellationToken);
            var last = _last;

            _last = current;

            if (_isInterceptedBefore)
            {
                return Equals(current, last) is false;
            }

            _isInterceptedBefore = true;

            return InterceptionResult.Continue();
        }
    }
}
