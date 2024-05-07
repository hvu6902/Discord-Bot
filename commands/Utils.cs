using Discord_App.other_command;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
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
        [Command("poll")]
        public async Task Poll(CommandContext context, string option1, string option2, string option3, string option4, [RemainingText] string pollTitle)
        {
            var interactivity = Program.Client.GetInteractivity();
            var pollTime = TimeSpan.FromSeconds(15);

            DiscordEmoji[] emo = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                   DiscordEmoji.FromName(Program.Client, ":two:"),
                                   DiscordEmoji.FromName(Program.Client, ":three:"),
                                   DiscordEmoji.FromName(Program.Client, ":four:") };

            string optionDescription = $"{emo[0]} | {option1} \n" +
                                       $"{emo[1]} | {option2} \n" +
                                       $"{emo[2]} | {option3} \n" +
                                       $"{emo[3]} | {option4} \n";

            var pollMsg = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Blue,
                Title = pollTitle,
                Description = optionDescription
            };

            var sentPoll = await context.Channel.SendMessageAsync(embed: pollMsg);
            foreach (var emoji in emo)
            {
                await sentPoll.CreateReactionAsync(emoji);
            }

            var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (var emoji in totalReactions)
            {
                if (emoji.Emoji == emo[0])
                {
                    count1++;
                }
                if (emoji.Emoji == emo[1])
                {
                    count2++;
                }
                if (emoji.Emoji == emo[2])
                {
                    count3++;
                }
                if (emoji.Emoji == emo[3])
                {
                    count4++;
                }
            }

            int totalVotes = count1 + count2 + count3 + count4;
            string result = $"{emo[0]}: {count1} votes \n" +
                            $"{emo[1]}: {count2} votes \n" +
                            $"{emo[2]}: {count3} votes \n" +
                            $"{emo[3]}: {count4} votes \n" +
                            $"Total votes = {totalVotes}";
            var resultEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Purple,
                Title = "Result of the Poll",
                Description = result
            };

            await context.Channel.SendMessageAsync(embed: resultEmbed);
        }
    }

}