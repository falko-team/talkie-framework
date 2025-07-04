using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Converters;
using Falko.Talkie.Flows;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Platforms;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Connections;

public sealed class TelegramUpdatePublisher(ISignalFlow flow, TelegramPlatform platform)
{
    private Sequence<TelegramMessage> _messagesGroup = [];

    private bool _isMessagesGroupAboutPublishedUpdates;

    public void Handle(IReadOnlyList<TelegramUpdate> updates, CancellationToken cancellationToken)
    {
        if (updates.Count is 0)
        {
            if (_messagesGroup.Count is 0) return;

            ProcessMessageGroup(cancellationToken);
        }

        foreach (var update in updates)
        {
            if (cancellationToken.IsCancellationRequested) break;

            if (update.Message is { } publishedMessage)
            {
                OnMessageUpdate(publishedMessage, true, cancellationToken);
            }
            if (update.BusinessMessage is { } publishedBusinessMessageMessage)
            {
                OnMessageUpdate(publishedBusinessMessageMessage, true, cancellationToken);
            }
            else if (update.EditedMessage is { } exchangedMessage)
            {
                OnMessageUpdate(exchangedMessage, false, cancellationToken);
            }
            else if (update.EditedBusinessMessage is { } exchangedBusinessMessage)
            {
                OnMessageUpdate(exchangedBusinessMessage, false, cancellationToken);
            }
        }
    }

    private void OnMessageUpdate(TelegramMessage message, bool published, CancellationToken cancellationToken)
    {
        if (_messagesGroup.Count is not 0 && IsMessageGroupAppendableTo(message, published) is false)
        {
            ProcessMessageGroup(cancellationToken);
        }

        if (message.MediaGroupId is null)
        {
            ProcessMessage(message, published, cancellationToken);
        }
        else
        {
            _messagesGroup.Add(message);
            _isMessagesGroupAboutPublishedUpdates = published;
        }
    }

    private bool IsMessageGroupAppendableTo(TelegramMessage message, bool published)
    {
        if (_isMessagesGroupAboutPublishedUpdates != published) return false;

        if (message.MediaGroupId is null) return false;

        var first = _messagesGroup.First();

        return message.MediaGroupId == first.MediaGroupId
           && message.Chat?.Id == first.Chat?.Id
           && message.From?.Id == first.From?.Id
           && message.SenderChat?.Id == first.SenderChat?.Id;
    }

    private void ProcessMessageGroup(CancellationToken cancellationToken)
    {
        if (_messagesGroup.TryGetIncomingMessage(platform, out var incomingMessage) is false)
        {
            return;
        }

        PublishMessage(incomingMessage, _isMessagesGroupAboutPublishedUpdates, cancellationToken);

        _messagesGroup = [];
    }

    private void ProcessMessage(TelegramMessage message, bool published, CancellationToken cancellationToken)
    {
        if (message.TryGetIncomingMessage(platform, out var incomingMessage) is false)
        {
            return;
        }

        PublishMessage(incomingMessage, published, cancellationToken);
    }

    private void PublishMessage(TelegramIncomingMessage incomingMessage, bool published, CancellationToken cancellationToken)
    {
        if (published)
        {
            flow.Publish(incomingMessage.ToMessagePublishedSignal(), cancellationToken);
        }
        else
        {
            flow.Publish(incomingMessage.ToMessageExchangedSignal(), cancellationToken);
        }
    }
}
