using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Reflection;

namespace DiscordBot
{
    public struct BotConfig
    {
        public string Token { get; set; }
    }

    class Bot
    {
        private DiscordSocketClient? _client; // 봇 클라이언트
        private InteractionService? _slashCommands;
        private BotConfig _botConfig;

        public async Task BotMain()
        {
            var botConfigJson = File.ReadAllText("../Config/Config.json");
            if (botConfigJson == null)
                return;

            _botConfig = JsonConvert.DeserializeObject<BotConfig>(botConfigJson);

            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            });
            _slashCommands = new InteractionService(_client.Rest, new InteractionServiceConfig()
            {
                LogLevel = LogSeverity.Verbose
            });

            _client.Log += OnClientLogReceived;
            _slashCommands.Log += OnClientLogReceived;

            await _client.LoginAsync(TokenType.Bot, _botConfig.Token);
            await _client.StartAsync();

            await _slashCommands.AddModulesAsync(Assembly.GetEntryAssembly(), null);  //봇에 명령어 모듈 등록

            await Task.Delay(Timeout.Infinite);
        }

        private Task OnClientLogReceived(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
