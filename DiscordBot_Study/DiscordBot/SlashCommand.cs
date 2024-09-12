using Discord.Interactions;

namespace DiscordBot
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ping", "live Check")]
        public async Task PingCommand()
        {
            await RespondAsync("pong");
        }
    }
}
