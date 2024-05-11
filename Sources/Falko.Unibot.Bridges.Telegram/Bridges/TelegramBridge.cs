using System.Text;
using System.Text.Json;
using Falko.Unibot.Bridges.Telegram.Models;
using Falko.Unibot.Bridges.Telegram.Serialization;
using Falko.Unibot.Collections;
using Falko.Unibot.Concurrent;

namespace Falko.Unibot.Bridges.Telegram.Bridges;

public sealed class TelegramBridge(string token) : IDisposable
{
    private readonly HttpClient _client = new();

    public async Task<Response<TOut>> SendAsync<TOut, TIn>(
        string method,
        TIn content,
        CancellationToken cancellationToken = default) where TIn : class where TOut : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.telegram.org/bot{token}/{method}")
        {
            Content = new StringContent(JsonSerializer.Serialize(content, typeof(TIn), ModelsJsonSerializerContext.Default), Encoding.UTF8, "application/json")
        };

        var response = await _client.SendAsync(request, cancellationToken);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        return (JsonSerializer.Deserialize(responseContent, typeof(Response<TOut>), ModelsJsonSerializerContext.Default) as Response<TOut>)!;
    }

    public void GetUpdatesLoop(Action<Update, CancellationToken> onUpdate,
        CancellationToken cancellationToken = default)
    {
        Task.Run(async () =>
        {
            long? offset = null;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var response =
                        await SendAsync<Update[], GetUpdates>("getUpdates", new GetUpdates(offset),
                            cancellationToken);

                    if (!response.Ok)
                    {
                        Console.WriteLine(response.Description);
                        continue;
                    }

                    if (response.Result.Any())
                    {
                        offset = response.Result.Last().UpdateId + 1;
                    }

                    await response.Result!.ToFrozenSequence().Parallelize()
                        .ForEachAsync(onUpdate, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }, cancellationToken);
    }

    public async Task<User> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var result = await SendAsync<User, object>("getMe", null!, cancellationToken);

        return result.Result!;
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
