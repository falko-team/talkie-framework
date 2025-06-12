namespace Falko.Talkie.Bridges.Telegram.Requests;

/// <summary>
/// Represents a request to the Telegram API.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ITelegramRequest<TResponse> : ITelegramRequest where TResponse : notnull;
