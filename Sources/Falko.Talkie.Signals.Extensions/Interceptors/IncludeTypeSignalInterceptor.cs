using Talkie.Signals;

namespace Talkie.Interceptors;

public sealed class IncludeTypeSignalInterceptor<T> : ISignalInterceptor
{
    public static readonly IncludeTypeSignalInterceptor<T> Instance = new();

    private IncludeTypeSignalInterceptor() { }

    public InterceptionResult Intercept(Signal signal, CancellationToken cancellationToken)
    {
        return signal is T;
    }
}
