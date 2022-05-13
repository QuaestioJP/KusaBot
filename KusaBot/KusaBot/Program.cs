using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace KusaBot
{
    class KusaBot
    {
        private readonly DiscordSocketClient _client;
        static void Main(string[] args)
        => new KusaBot()
        .MainAsync()
        .GetAwaiter()
        .GetResult();

        public KusaBot()
        {
            _client = new DiscordSocketClient();

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            Console.Write("Token: ");
            string Token = Console.ReadLine();
            await _client.LoginAsync(TokenType.Bot, Token);

            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            //メッセージがBot自身の場合無視する
            if (message.Author.Id == _client.CurrentUser.Id)
                return;

            //内容がtestの場合
            if (message.Content == "test")
            {
                await message.Channel.SendMessageAsync("BotTest");
            }
        }
    }
}