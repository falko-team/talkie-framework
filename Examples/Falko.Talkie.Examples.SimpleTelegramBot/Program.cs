using Microsoft.Extensions.Logging;
using Talkie.Collections;
using Talkie.Common;
using Talkie.Concurrent;
using Talkie.Controllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Models.Messages;
using Talkie.Models.Profiles;
using Talkie.Pipelines;
using Talkie.Signals;

// Get telegram token from command line arguments.
var telegramToken = string.Join(' ', args).Trim();

// Used stacked for disposing last to first disposables.
// At this example first disposed flow and then disposed telegram connection.
await using var disposables = new DisposableStack();

// Create signal flow and dispose it with disposables.
var flow = new SignalFlow()
    .DisposeWith(disposables);

// Create Microsoft Logger Factory.
var loggerFactory = LoggerFactory.Create(builder => builder
        .AddConsole() // Add console as logging target
        .AddDebug() // Add debug output as logging target
        .SetMinimumLevel(LogLevel.Debug)) // set minimum level to log
    .DisposeWith(disposables);

// Create logger for signal flow.
var signalFlowLogger = loggerFactory.CreateLogger<SignalFlow>();

// Create subscription for all signals and log them.
flow.Subscribe(builder => builder
    .Handle(context => signalFlowLogger
        .LogDebug("Signal received: {Signal}", context.Signal))); // log any signals

// Create task completion source for waiting flow completion.
var flowWaiter = new TaskCompletionSource();

// Create subscription for unhanded exception signal.
// When exception received, set flow as exception state.
flow.Subscribe<UnhandledExceptionSignal>(builder => builder
    .Handle(context => flowWaiter
        .SetException(context.Signal.Exception))); // set exception to waiter

// Create subscription for incoming message signal with command "/hello".
// When command received, send message "hi".
flow.Subscribe<TelegramIncomingMessageSignal>(builder => builder
    .Where(signal => IsTelegramCommand(signal.Message, "hello")) // only messages with command "/hello"
    .HandleAsync(context => context
        .ToMessageController() // get message controller
        .PublishMessageAsync("hi") // send message "hi"
        .AsValueTask()));

// Echo message text back to the sender only in private chats example pipeline.
flow.Subscribe<IncomingMessageSignal>(builder => builder
    .Where(signal => signal.Message.Entry.Environment is IUserProfile) // only chat with user
    .Where(signal => signal.Message.Content.IsNullOrWhiteSpace() is false) // only where message text is not empty
    .Select(signal => signal.Mutate(mutator => mutator
        .ContentMutation(content => content?.Trim().ToLowerInvariant())))
    .HandleAsync(context => context
        .ToMessageController() // get message controller
        .PublishMessageAsync(context.Signal.Message.Content!) // send message with same text to the chat of sender
        .AsValueTask()));

// Connect to telegram with empty token and dispose it with disposables.
// Added you token to connect to telegram!
await flow.ConnectTelegramAsync(telegramToken)
    .DisposeAsyncWith(disposables);

// Wait for flow completion.
await flowWaiter.Task;

return;

// Define helper method to check if message is telegram command.
static bool IsTelegramCommand(IMessage message, string command)
{
    return message is { Content: { } content}
        && content
            .TrimStart()
            .StartsWith($"/{command}", StringComparison.InvariantCultureIgnoreCase);
}
