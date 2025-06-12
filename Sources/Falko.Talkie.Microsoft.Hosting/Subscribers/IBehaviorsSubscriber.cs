using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;

namespace Falko.Talkie.Subscribers;

public interface IBehaviorsSubscriber
{
    void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken);
}
