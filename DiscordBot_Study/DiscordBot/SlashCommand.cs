using Discord.Interactions;

namespace DiscordBot
{
    public class IteractionHandler
    {

    }

    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("ping", "live Check")]
        public async Task PingCommand()
        {
            await RespondAsync("pong");
        }
    }
}
