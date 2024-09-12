using DiscordBot;

namespace DiscordBot_Study
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            new Bot().BotMain().GetAwaiter().GetResult();
        }
    }
}