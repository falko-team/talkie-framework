using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Models;
using Talkie.Converters;
using Talkie.Flows;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Signals;
using Talkie.Validations;

namespace Talkie.Connections;

public sealed class TelegramSignalConnection(ISignalFlow flow, string token) : ISignalConnection
{
    private CancellationTokenSource? _globalCancellationTokenSource;

    private Task? _processUpdatesTask;

    public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
    {
        _globalCancellationTokenSource = new();

        token.ThrowIf()!.NullOrWhiteSpace();

        var client = new TelegramBotApiClient(token);

        IBotProfile self;

        try
        {
            self = GetSelf(await client.GetMeAsync(cancellationToken));
        }
        catch (TelegramBotApiRequestException exception)
        {
            if (exception.Code is not 401) throw;

            throw new UnauthorizedAccessException("Invalid token");
        }

        var platform = new TelegramPlatform(client, self);

        _processUpdatesTask = client.ProcessUpdatesAsync((update, scopedCancellationToken) =>
                ProcessUpdate(platform, update, scopedCancellationToken),
            _globalCancellationTokenSource.Token);
    }

    private async ValueTask ProcessUpdate(TelegramPlatform platform, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message) return;

        var incomingMessage = IncomingMessageConverter.Convert(platform, message);

        if (incomingMessage is null) return;

        await flow.PublishAsync(new TelegramIncomingMessageSignal(incomingMessage), cancellationToken);
    }

    private static IBotProfile GetSelf(User self)
    {
        return new TelegramBotProfile
        {
            Id = self.Id,
            FirstName = self.FirstName,
            LastName = self.LastName,
            NickName = self.Username
        };
    }

    public async ValueTask DisposeAsync()
    {
        if (_globalCancellationTokenSource is not null)
        {
            await _globalCancellationTokenSource.CancelAsync();
            _globalCancellationTokenSource.Dispose();
        }

        if (_processUpdatesTask is not null)
        {
            await _processUpdatesTask;
        }
    }
}
