using Talkie.Disposables;

namespace Talkie.Flows;

public static partial class SubscriptionExtensions
{
    public static void UnsubscribeWith(this Subscription subscription,
        IRegisterOnlyDisposableScope disposables)
    {
        disposables.Register(new SubscriptionDisposableWrapper(subscription));
    }
}

file sealed class SubscriptionDisposableWrapper(Subscription subscription) : IAsyncDisposable
{
    public ValueTask DisposeAsync()
    {
        subscription.Remove();

        return ValueTask.CompletedTask;
    }
}
