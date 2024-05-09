using Falko.Unibot.Collections;
using Falko.Unibot.Disposables;
using Falko.Unibot.Controllers;
using Falko.Unibot.Flows;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Pipelines;
using Falko.Unibot.Signals;
using Microsoft.Extensions.Logging;

var telegramToken = args[0];

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
    .Handle(context => context
        .ToOutgoingMessageController()
        .SendAsync(b => b
            .AddText("hi"))
        .Wait()));

// Create subscription for incoming message signal with not empty content.
// When message received, send message with same content.
flow.Subscribe(builder => builder
    .OfType<IncomingMessageSignal>()
    .Where(signal => signal.Message.WithEntry()?.Receiver is IUserProfile)
    .Where(signal => string.IsNullOrWhiteSpace(signal.Message.Content) is false)
    .Handle(context => context
        .ToOutgoingMessageController()
        .SendAsync(b => b
            .AddText(context.Signal.Message.Content!))
        .Wait()));

// Connect to telegram with empty token and dispose it with disposables.
// Added you token to connect to telegram!
await flow.ConnectTelegramAsync(telegramToken)
    .DisposeAsyncWith(disposables);

// Wait for flow completion.
await flowWaiter.Task;
