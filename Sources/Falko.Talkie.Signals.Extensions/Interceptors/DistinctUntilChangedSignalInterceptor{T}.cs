using Falko.Talkie.Signals;

namespace Falko.Talkie.Interceptors;

public sealed class DistinctUntilChangedSignalInterceptor<TSignal, TValue>(Func<TSignal, CancellationToken, TValue> distinctUntilChanged)
        : SignalInterceptor<TSignal>
    where TSignal : Signal
{
    private readonly object _locker = new();

    private bool _isInterceptedBefore;

    private TValue _last = default!;

    public override InterceptionResult Intercept(TSignal signal, CancellationToken cancellationToken)
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
