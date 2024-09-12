using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    class Bot
    {
        private DiscordSocketClient _client; // 봇 클라이언트

        private static IConfiguration _configuration;
        private static IServiceProvider _services;

        private static readonly DiscordSocketConfig _discordSocketConfig = new()
        {
            LogLevel = LogSeverity.Verbose,
            GatewayIntents = GatewayIntents.MessageContent
        };

        private static readonly InteractionServiceConfig _interactionServiceConfig = new()
        {
            LogLevel = LogSeverity.Verbose
        };

        public async Task BotMain()
        {
            // Discord Config 로드
            _configuration = new ConfigurationBuilder().
                SetBasePath(AppContext.BaseDirectory).
                AddJsonFile("Config.json").
                Build();

            _services = new ServiceCollection()
                .AddSingleton(_configuration)
                .AddSingleton(_discordSocketConfig)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(), _interactionServiceConfig))
                .AddSingleton<InteractionHandler>()
                .BuildServiceProvider();

            _client = _services.GetRequiredService<DiscordSocketClient>();
            _client.Log += LogAsync;

            await _services.GetRequiredService<InteractionHandler>().InitializeAsync();

            await _client.LoginAsync(TokenType.Bot, _configuration["Token"]);
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
