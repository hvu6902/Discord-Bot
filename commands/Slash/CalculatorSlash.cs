using DSharpPlus.Entities;
using DSharpPlus.Net.Models;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App.commands.Slash
{
    [SlashCommandGroup("calc", "Perform calculator operations")]
    public class CalculatorSlash : ApplicationCommandModule
    {
        [SlashCommand("add", "Add 2 numbers together")]
        public async Task Add(InteractionContext ctx, [Option("number1", "Enter first number")] double number1, [Option("number2", "Enter second number")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Lilac,
                Title = $"The magic operation is {number1} + {number2}",
                Description = $"Sums of those 2 are: {number1 +  number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }
        [SlashCommand("sub", "Substract 2 numbers")]
        public async Task Sub(InteractionContext ctx, [Option("number1", "Enter first number")] double number1, [Option("number2", "Enter second number")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Lilac,
                Title = $"The magic operation is {number1} - {number2}",
                Description = $"The difference of those 2 is {number1 - number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }

        [SlashCommand("mult", "Multiply 2 numbers")]
        public async Task Multiply(InteractionContext ctx, [Option("number1", "Enter first number")] double number1, [Option("number2", "Enter second number")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Lilac,
                Title = $"The magic operation is {number1} * {number2}",
                Description = $"The product of those 2 is {number1 * number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }

        [SlashCommand("div", "Divide 2 numbers")]
        public async Task Divide(InteractionContext ctx, [Option("number1", "Enter first number")] double number1, [Option("number2", "Enter second number")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Lilac,
                Title = $"The magic operation is {number1} / {number2}",
                Description = $"The quotient of those 2 is {number1 / number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }
    }
}
