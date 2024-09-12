using DiscordBot;

namespace Program
{
    class MainApp
    {
        static void Main(string[] args)
        {
            new Bot().BotMain().GetAwaiter().GetResult();
        }
    }
}