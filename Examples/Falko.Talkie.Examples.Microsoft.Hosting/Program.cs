// IWith и в чем отличие от обычных интерфейсов
// Sequence/FrozenSequence и отличие от List
// IDisposable и IAsyncDisposable, и IDisposableScope и IRegisterOnlyDisposableScope
// foreach
// Parallelize().ForEach
// async/await

using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Configurations;

var configuration = new TelegramConfiguration("TOKEN");

var client = new TelegramClient(configuration);

var self = await client.GetMeAsync();
