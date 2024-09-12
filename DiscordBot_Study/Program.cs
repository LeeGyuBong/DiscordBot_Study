using Discord.Interactions;
using Discord.WebSocket;
using Discord;
using DiscordBot;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

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