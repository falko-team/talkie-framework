using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class BufferSignalInterceptor<T>(TimeSpan delay) : ISignalInterceptor<T> where T : Signal
{
    private readonly object _locker = new();

    private Task? _delayTask;

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        try
        {
            lock (_locker)
            {
                _delayTask ??= Task
                    .Delay(delay, cancellationToken)
                    .ContinueWith(_ => _delayTask = null, cancellationToken);
            }

            _delayTask?.Wait(cancellationToken);

            return InterceptionResult.Continue();
        }
        catch
        {
            return InterceptionResult.Break();
        }
    }

    InterceptionResult ISignalInterceptor<T>.Intercept(T signal, CancellationToken cancellationToken)
    {
        return Intercept(signal, cancellationToken);
    }
}
