namespace Falko.Talkie.Flows;

public readonly struct Subscription(Action remove)
{
    public void Remove() => remove();
}
