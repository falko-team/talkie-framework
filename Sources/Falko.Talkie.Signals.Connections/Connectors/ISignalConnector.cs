using Talkie.Connections;
using Talkie.Flows;

namespace Talkie.Connectors;

public interface ISignalConnector
{
    /// <summary>
    /// Connects the connector to a <see cref="ISignalFlow"/>.
    /// </summary>
    /// <param name="flow">The <see cref="ISignalFlow"/> to connect to.</param>
    /// <returns>The <see cref="ISignalConnection"/> representing the connection.</returns>
    ISignalConnection Connect(ISignalFlow flow);
}
