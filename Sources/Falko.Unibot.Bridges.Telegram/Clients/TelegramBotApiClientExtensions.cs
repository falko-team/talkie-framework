using Falko.Unibot.Bridges.Telegram.Models;
using Falko.Unibot.Collections;
using Falko.Unibot.Concurrent;

namespace Falko.Unibot.Bridges.Telegram.Clients;

public static class TelegramBotApiClientExtensions
{
    public static Task ProcessUpdatesAsync(this ITelegramBotApiClient client, Action<Update, CancellationToken> process,
        CancellationToken cancellationToken = default)
    {
        return Task.Factory.StartNew(async () =>
        {
            long? offset = null;

            while (cancellationToken.IsCancellationRequested is false)
            {
                var updates = await client.GetUpdatesAsync(new GetUpdates(offset), cancellationToken);

                if (updates.Length is 0) continue;

                offset = updates[^1].UpdateId + 1;

                await updates.ToFrozenSequence().Parallelize().ForEachAsync(process, cancellationToken: cancellationToken);
            }
        }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public static Task<Update[]> GetUpdatesAsync(this ITelegramBotApiClient client, GetUpdates getUpdates, CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Update[], GetUpdates>("getUpdates", getUpdates, cancellationToken);
    }

    public static Task<User> GetMeAsync(this ITelegramBotApiClient client, CancellationToken cancellationToken = default)
    {
        return client.SendAsync<User>("getMe", cancellationToken);
    }

    public static Task<Message> SendMessageAsync(this ITelegramBotApiClient client, SendMessage message, CancellationToken cancellationToken = default)
    {
        return client.SendAsync<Message, SendMessage>("sendMessage", message, cancellationToken);
    }
}
