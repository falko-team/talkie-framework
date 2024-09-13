using Talkie.Disposables;
using Talkie.Flows;

namespace Talkie.Subscribers;

public interface IBehaviorsSubscriber
{
    void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken);
}
