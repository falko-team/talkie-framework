using Falko.Unibot.Signals;

namespace Falko.Unibot.Interceptors;

public readonly struct InterceptionResult
{
    private InterceptionResult(bool canContinue, Signal? replacedSignal = null)
    {
        CanContinue = canContinue;
        ReplacedSignal = replacedSignal;
    }

    internal bool CanContinue { get; } = true;

    internal Signal? ReplacedSignal { get; }

    public static InterceptionResult ContinueWith(Signal signal)
    {
        return new InterceptionResult(true, signal);
    }

    public static InterceptionResult Continue()
    {
        return new InterceptionResult(true);
    }

    public static InterceptionResult Break()
    {
        return new InterceptionResult(false);
    }

    public static implicit operator InterceptionResult(bool boolean)
    {
        return boolean ? Continue() : Break();
    }
}
