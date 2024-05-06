using Discord_App.commands;
using Discord_App.commands.Slash;
using Discord_App.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
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

            // Set up Ready Event-Handler
            Client.Ready += OnClientReady;
            Client.VoiceStateUpdated += VoiceChannelHandler;

            // Create Command Config
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            //Initializing CommandsNextExtention property
            Commands = Client.UseCommandsNext(commandsConfig);

            // Enabling the Client to use Slash Commands
            var slashCommandsConfig = Client.UseSlashCommands();

            Commands.CommandErrored += CommandsHandler;
            
            // Register Commands Classes
            Commands.RegisterCommands<Utils>();

            slashCommandsConfig.RegisterCommands<SlashCommands>();
            slashCommandsConfig.RegisterCommands<CalculatorSlash>();
            
            //Connect to the Bot online
            await Client.ConnectAsync();
            // The delay is set to -1 to ensure the bot running forever
            await Task.Delay(-1);

        }
        // Command spamming handler
        private static async Task CommandsHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException exception)
            {
                string timeLeft = string.Empty;

                foreach (var check in exception.FailedChecks)
                {
                    var coolDown = (CooldownAttribute)check;
                    timeLeft = coolDown.GetRemainingCooldown(e.Context).ToString(@"hh\:mm\:ss");
                }

                var coolDownMsg = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "Are you in a hurry or sth?",
                    Description = $"Time remaining: {timeLeft}"
                };

                await e.Context.Channel.SendMessageAsync(embed: coolDownMsg);
            }
        }

        // Handling Voice Channel
        private static async Task VoiceChannelHandler(DiscordClient sender, VoiceStateUpdateEventArgs e)
        {
            if (e.Before == null && e.Channel.Name == "General")
            {
                await e.Channel.SendMessageAsync($"{e.User.Username} just joined the Voice Channel");
            }
        }

        private static Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
