using Talkie.Models.Messages;
using Talkie.Platforms;
using Talkie.Signals;

namespace Talkie.Pipelines.Intercepting;

public static partial class SignalInterceptingPipelineBuilderExtensions
{
    public static ISignalInterceptingPipelineBuilder<MessagePublishedSignal> OnlyCommands
    (
        this ISignalInterceptingPipelineBuilder<MessagePublishedSignal> builder,
        string command
    )
    {
        return builder.Where(signal =>
        {
            var message = signal.Message;
            var text = message.GetText();

            return message
                .Platform
                .GetCommandControllerFactory()
                .Create(text)
                .IsCommand(command);
        });
    }

    public static ISignalInterceptingPipelineBuilder<MessageExchangedSignal> OnlyCommands
    (
        this ISignalInterceptingPipelineBuilder<MessageExchangedSignal> builder,
        string command
    )
    {
        return builder.Where(signal =>
        {
            var message = signal.Message;
            var text = message.GetText();

            return message
                .Platform
                .GetCommandControllerFactory()
                .Create(text)
                .IsCommand(command);
        });
    }
}
