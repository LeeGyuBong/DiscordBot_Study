using Discord.Interactions;
using Discord;
using Discord.WebSocket;
using DiscordBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot_Study
{
    public class Program
    {
        // Discord Config 로드
        public static readonly IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("Config.json").Build();

        public static IServiceProvider? Services;

        private static readonly DiscordSocketConfig _discordSocketConfig = new()
        {
            LogLevel = LogSeverity.Verbose,
            GatewayIntents = GatewayIntents.MessageContent
        };

        private static readonly InteractionServiceConfig _interactionServiceConfig = new()
        {
            LogLevel = LogSeverity.Verbose
        };

        public static void Main(string[] args)
        {
            Services = new ServiceCollection()
                .AddSingleton(Configuration)
                .AddSingleton(_discordSocketConfig)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(), _interactionServiceConfig))
                .AddSingleton<InteractionHandler>()
                .BuildServiceProvider();

            new Bot().BotMain().GetAwaiter().GetResult();
        }
    }
}