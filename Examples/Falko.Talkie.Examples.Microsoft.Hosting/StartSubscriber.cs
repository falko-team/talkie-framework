using Talkie.Concurrent;
using Talkie.Controllers.MessageControllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Models.Messages;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Outgoing;
using Talkie.Models.Profiles;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;
using Talkie.Subscribers;

namespace Talkie.Examples;

public sealed class StartSubscriber : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(static signals => signals
            .SkipSelfPublish()
            .Where(signal => signal
                .Message
                .GetText()
                .TrimStart()
                .StartsWith("/start", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync((context, cancellationToken) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                    .SetReply(context.GetMessage())
                    .SetContent(content => content
                        .AddText(nameof(Talkie), BoldTextStyle.FromRange)
                        .AddText(" is a library for building chatbots in .NET.", ItalicTextStyle.FromRange)),
                    cancellationToken)
                .AsValueTask())
            .HandleAsync((context, cancellationToken) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                    .SetReply(context.GetMessage())
                    .SetContent(content => content
                        .AddText("Hello, ", BoldTextStyle.FromRange)
                        .AddText(GetProfileDisplayName(context
                            .GetMessage()
                            .PublisherProfile), MonospaceTextStyle.FromRange)),
                    cancellationToken)
                .AsValueTask()))
            .UnsubscribeWith(disposables);
    }

    private static string GetProfileDisplayName(IProfile profile)
    {
        return profile switch
        {
            IUserProfile user => user.FirstName ?? user.NickName,
            IChatProfile chat => chat.Title ?? chat.NickName,
            _ => null
        } ?? "Developer";
    }
}
