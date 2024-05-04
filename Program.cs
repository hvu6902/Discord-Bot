using Discord_App.commands;
using Discord_App.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App
{
    public sealed class Program
    {
        public static DiscordClient Client { get; private set; }
        public static CommandsNextExtension Commands {  get; private set; }
        static async Task Main(string[] args)
        {
            // Get details from config.json
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            //Set up bot config
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            // Apply config to Discord client
            Client = new DiscordClient(discordConfig);

            // Set default time out for Commands that use Interactivity
            Client.UseInteractivity(new InteractivityConfiguration() 
            {
                Timeout = TimeSpan.FromMinutes(1.5)
            });

            // Set up Task Handler Ready event
            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            // Register Commands
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<Utils>();

            await Client.ConnectAsync();
            await Task.Delay(-1);

        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
