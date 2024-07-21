using Microsoft.Extensions.Logging;
using Talkie.Collections;
using Talkie.Common;
using Talkie.Concurrent;
using Talkie.Controllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Models.Messages;
using Talkie.Models.Profiles;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;
using Talkie.Validations;

// Get telegram token from command line arguments.
var telegramToken = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");

telegramToken.ThrowIf()!.NullOrWhiteSpace();

// Used stacked for disposing last to first disposables.
// At this example first disposed flow and then disposed telegram connection.
await using var disposables = new DisposableStack();

// Create signal flow and dispose it with disposables.
var flow = new SignalFlow()
    .DisposeWith(disposables);

// Create unobserved exception signal task.
var unobservedExceptionTask = flow.TakeAsync(signals => signals
    .Only<UnobservedConnectionExceptionSignal, UnobservedPublishingExceptionSignal>())
    .ContinueWith(signal => signal.Result switch
    {
        UnobservedPublishingExceptionSignal publishingExceptionSignal => publishingExceptionSignal.Exception,
        UnobservedConnectionExceptionSignal connectionExceptionSignal => connectionExceptionSignal.Exception,
        _ => new InvalidOperationException("Unknown unobserved exception signal")
    }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion);

// Create Microsoft Logger Factory.
var loggerFactory = LoggerFactory.Create(signals => signals
        .AddConsole() // Add console as logging target
        .AddDebug() // Add debug output as logging target
        .SetMinimumLevel(LogLevel.Debug)) // set minimum level to log
    .DisposeWith(disposables);

// Create logger for signal flow.
var signalFlowLogger = loggerFactory.CreateLogger<SignalFlow>();

// Create subscription for all signals and log them
flow.Subscribe(signals => signals
    .Handle(context => signalFlowLogger
        .LogDebug("Signal received: {Signal}", context.Signal))); // log any signals

// Create subscription for incoming message signal with command "/hello".
// When command received, send message "hi".
flow.Subscribe<TelegramIncomingMessageSignal>(signals => signals
    .Where(signal => IsTelegramCommand(signal.Message, "hello")) // only messages with command "/hello"
    .HandleAsync(context => context
        .ToMessageController() // get message controller
        .PublishMessageAsync("hi") // send message "hi"
        .AsValueTask()));

// Echo message text back to the sender only in private chats example pipeline
flow.Subscribe<IncomingMessageSignal>(signals => signals
    .Where(signal => signal.Message.EnvironmentProfile is IUserProfile) // only chat with user
    .Where(signal => signal.Message.Text.IsNullOrWhiteSpace() is false) // only where message text is not empty
    .Select(signal => signal.MutateMessage(mutator => mutator
        .MutateText(content => content?.Trim().ToLowerInvariant())))
    .HandleAsync(context => context
        .ToMessageController() // get message controller
        .PublishMessageAsync(context.Signal.Message.Text!) // send message with same text to the chat of sender
        .AsValueTask()));

// Connect to telegram with empty token and dispose it with disposables.
// Added you token to connect to telegram!
await flow.ConnectTelegramAsync(telegramToken)
    .DisposeAsyncWith(disposables);

// Wait for first unobserved publishing exceptions.
throw await unobservedExceptionTask;

// Define helper method to check if message is telegram command.
static bool IsTelegramCommand(IMessage message, string command)
{
    return message is { Text: { } content}
        && content
            .TrimStart()
            .StartsWith($"/{command}", StringComparison.InvariantCultureIgnoreCase);
}
