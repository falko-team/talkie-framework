using Talkie.Concurrent;
using Talkie.Controllers.MessageControllers;
using Talkie.Disposables;
using Talkie.Flows;
using Talkie.Handlers;
using Talkie.Models.Messages;
using Talkie.Models.Messages.Attachments;
using Talkie.Models.Messages.Attachments.Variants;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Contents.Styles;
using Talkie.Models.Messages.Outgoing;
using Talkie.Models.Profiles;
using Talkie.Pipelines.Handling;
using Talkie.Pipelines.Intercepting;
using Talkie.Signals;
using Talkie.Subscribers;

namespace Talkie.Examples;

public sealed class StickerDownloaderSubscriber : IBehaviorsSubscriber
{
    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(signals => signals
            .SkipSelfPublish()
            .Where(signal => signal
                .Message
                .GetText()
                .TrimStart()
                .StartsWith("/start", StringComparison.InvariantCultureIgnoreCase))
            .HandleAsync((context, cancellation) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                    .SetReply(context.GetMessage())
                    .SetContent(content => content
                        .AddText(nameof(Talkie), BoldTextStyle.FromTextRange)
                        .AddText(" is a library for building chatbots in .NET.", ItalicTextStyle.FromTextRange)),
                    cancellation)
                .AsValueTask())
            .HandleAsync((context, cancellation) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                    .SetReply(context.GetMessage())
                    .SetContent(content => content
                        .AddText("Hello, ", BoldTextStyle.FromTextRange)
                        .AddText(GetProfileDisplayName(context
                            .GetMessage()
                            .PublisherProfile), MonospaceTextStyle.FromTextRange)),
                    cancellation)
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
