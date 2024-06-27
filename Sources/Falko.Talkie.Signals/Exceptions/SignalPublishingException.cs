using System.Text;
using Talkie.Flows;

namespace Talkie.Exceptions;

public sealed class SignalPublishingException(ISignalFlow flow, Exception? innerException = null)
    : Exception(innerException?.Message, innerException)
{
    public ISignalFlow Flow { get; } = flow;

    public override string Message => GetFormattedMessage();

    private string GetFormattedMessage()
    {
        var strings = new StringBuilder();

        strings.AppendLine("An exception occurred while publishing signal");

        if (InnerException is not null)
        {
            strings.AppendLine($": '{base.Message}'");
        }

        return strings.ToString();
    }
}
