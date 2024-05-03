using Discord_App.other_command;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App.commands
{
    internal class Utils : BaseCommandModule
    {
        [Command("test")]
        public async Task FirstCommand(CommandContext context)
        {
            await context.Channel.SendMessageAsync($"Dit me may {context.User.Username}");
        }

        [Command("add")]
        public async Task Add(CommandContext context, int number1, int number2)
        {
            int result = number1 + number2;
            await context.Channel.SendMessageAsync(result.ToString());
        }

        [Command("embed")]
        public async Task EmbedMsg(CommandContext context)
        {
            var msg = new DiscordEmbedBuilder
            {
                Title = "Thong bao",
                Description = $"Me thang {context.User.Username} beo vkl",
                Color = DiscordColor.Blue,
            };

            await context.Channel.SendMessageAsync(embed: msg);
        }

        [Command("card")]
        public async Task CardGame(CommandContext context)
        {
            var userCard = new CardSystem();

            var userCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"Bai cua may la {userCard.SelectedCard} ",
                Color = DiscordColor.Yellow,
            };

            await context.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCard = new CardSystem();

            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"Bai cua tao la {botCard.SelectedCard}",
                Color = DiscordColor.Lilac,
            };

            await context.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.CardPowerNum > botCard.CardPowerNum)
            {
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = "May thang roi nhe",
                    Color = DiscordColor.Turquoise,
                };

                await context.Channel.SendMessageAsync(embed: winMessage);
            }
            else if (userCard.CardPowerNum == botCard.CardPowerNum && userCard.CardPowerSuits > botCard.CardPowerSuits)
            {
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = "May thang roi nhe",
                    Color = DiscordColor.Turquoise,
                };

                await context.Channel.SendMessageAsync(embed: winMessage);
            }
            else
            {
                var loseMessage = new DiscordEmbedBuilder
                {
                    Title = "Alexa play Loser by BigBang",
                    Color = DiscordColor.Red,
                };

                await context.Channel.SendMessageAsync(embed: loseMessage);
            }
        }
    }
}