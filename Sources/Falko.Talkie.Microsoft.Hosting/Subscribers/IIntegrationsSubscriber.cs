using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;

namespace Falko.Talkie.Subscribers;

public interface IIntegrationsSubscriber
{
    Task SubscribeAsync(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken);
}
