using Falko.Talkie.Concurrent;
using Falko.Talkie.Controllers.AttachmentControllers;
using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Disposables;
using Falko.Talkie.Flows;
using Falko.Talkie.Handlers;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Contents.Styles;
using Falko.Talkie.Models.Messages.Outgoing;
using Falko.Talkie.Models.Profiles;
using Falko.Talkie.Pipelines.Handling;
using Falko.Talkie.Pipelines.Intercepting;
using Falko.Talkie.Signals;
using Falko.Talkie.Subscribers;

namespace Falko.Talkie.Examples;

public sealed class StartSubscriber : IBehaviorsSubscriber
{
    private const string RepositoryUrl = "https://github.com/falko-team/talkie-framework";
    private const string CoverUrl = "https://github.com/falko-team/talkie-framework/blob/ea4b18348782b2bcf9fead45b5963b5f76d79ecb/Logo512.png?raw=true";

    public void Subscribe(ISignalFlow flow, IRegisterOnlyDisposableScope disposables, CancellationToken cancellationToken)
    {
        flow.Subscribe<MessagePublishedSignal>(signals => signals
            .OnlyCommands("/start")
            .HandleAsync((context, cancellation) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                    .SetReply(context.GetMessage())
                    .SetContent(content => content
                        .AddText(nameof(Talkie), BoldTextStyle.FromTextRange)
                        .AddText(" is a library for building chatbots in .NET.", ItalicTextStyle.FromTextRange, BoldTextStyle.FromTextRange)),
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
                .AsValueTask())
            .HandleAsync((context, cancellation) => context
                .ToMessageController()
                .PublishMessageAsync(message => message
                        .SetReply(context.GetMessage())
                        .AddAttachment(context.GetAttachmentController()
                            .ImageAttachment.Build(CoverUrl))
                        .SetContent(content => content
                            .AddText("Source Code", LinkTextStyle.FromLink(RepositoryUrl).FromTextRange)),
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
        } ?? "Unknown";
    }
}
