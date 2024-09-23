using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DiscordBot_Study;

namespace DiscordBot
{
    class Bot
    {
        private DiscordSocketClient? _client; // 봇 클라이언트

        public async Task BotMain()
        {
            _client = Program.Services.GetRequiredService<DiscordSocketClient>();
            _client.Log += LogAsync;

            await Program.Services.GetRequiredService<InteractionHandler>().InitializeAsync();

            await _client.LoginAsync(TokenType.Bot, Program.Configuration["DiscordToken"]);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        public static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}
