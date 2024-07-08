using Talkie.Flows;

namespace Talkie.Exceptions;

public sealed class SignalPublishingException(ISignalFlow flow, Exception innerException)
    : Exception(innerException.Message, innerException)
{
    public ISignalFlow Flow { get; } = flow;

    public override string Message => $"An exception occurred while publishing signal: '{base.Message}'";
}
