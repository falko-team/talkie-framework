using Talkie.Disposables;
using Talkie.Flows;

namespace Talkie.Subscribers;

public interface IIntegrationsSubscriber
{
    Task SubscribeAsync(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken);
}
