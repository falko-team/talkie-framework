namespace Talkie.Bridges.Telegram.Requests;

/// <summary>
/// Represents a request to the Telegram API.
/// </summary>
/// <typeparam name="T">The type of the response.</typeparam>
public interface ITelegramRequest<T> where T : notnull;
