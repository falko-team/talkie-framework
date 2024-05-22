using Talkie.Signals;

namespace Talkie.Interceptors;

public readonly struct InterceptionResult
{
    public readonly bool CanContinue;

    public readonly Signal? ReplacedSignal;

    private InterceptionResult(bool canContinue, Signal? replacedSignal = null)
    {
        CanContinue = canContinue;
        ReplacedSignal = replacedSignal;
    }

    public static InterceptionResult ContinueWith(Signal signal) => new(true, signal);

    public static InterceptionResult Continue() => new(true);

    public static InterceptionResult Break() => new(false);

    public static implicit operator InterceptionResult(bool boolean) => boolean ? Continue() : Break();

    public static implicit operator InterceptionResult(Signal signal) => ContinueWith(signal);
}
