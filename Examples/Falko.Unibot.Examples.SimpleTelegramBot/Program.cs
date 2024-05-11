using Falko.Unibot.Collections;
using Falko.Unibot.Common;
using Falko.Unibot.Disposables;
using Falko.Unibot.Controllers;
using Falko.Unibot.Flows;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Pipelines;
using Falko.Unibot.Platforms;
using Falko.Unibot.Signals;
using Microsoft.Extensions.Logging;

var telegramToken = args[0].Trim();

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
        .LogDebug("Signal received: {Signal}", context.Signal)));

// Create task completion source for waiting flow completion.
var flowWaiter = new TaskCompletionSource();

// Create subscription for unhanded exception signal.
// When exception received, set flow as exception state.
flow.Subscribe(builder => builder
    .OfType<UnhandledExceptionSignal>()
    .Handle(context => flowWaiter
        .SetException(context.Signal.Exception)));

// Create subscription for incoming message signal with command "/hello".
// When command received, send message "hi".
flow.Subscribe(builder => builder
    .OfType<IncomingMessageSignal>()
    .Where(signal => signal.Message.Content?.Trim().StartsWith("/hello") is true)
    .HandleAsync(context => context
        .ToMessageController()
        .PublishMessageAsync(b => b
            .AddText("hi"))));

// Echo message text back to the sender only in private chats example pipeline.
flow.Subscribe(builder => builder
    .OfType<IncomingMessageSignal>() // new messages only
    .Where(signal => signal.Message.OfPlatform<TelegramPlatform>()) // telegram messages only
    .Where(signal => signal.Message.WithEntry()?.Receiver is IUserProfile) // only in telegram chat with user
    .Where(signal => signal.Message.Content.IsNullOrWhiteSpace() is false) // only where message text is not empty
    .HandleAsync(context => context
        .ToMessageController()
        .PublishMessageAsync(context.Signal.Message.Content!))); // send message with same text to the chat of sender

// Connect to telegram with empty token and dispose it with disposables.
// Added you token to connect to telegram!
await flow.ConnectTelegramAsync(telegramToken)
    .DisposeAsyncWith(disposables);

// Wait for flow completion.
await flowWaiter.Task;
