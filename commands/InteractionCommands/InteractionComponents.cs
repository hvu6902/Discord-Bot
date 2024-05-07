using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App.commands.InteractionCommands
{
    public class InteractionComponents : BaseCommandModule
    {
        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var basicsBtn = new DiscordButtonComponent(ButtonStyle.Primary, "basicsBtn", "Basics");
            var calcBtn = new DiscordButtonComponent(ButtonStyle.Success, "calcBtn", "Calculator");

            var msg = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.White)
                    .WithTitle("Help Secttion")
                    .WithDescription("Press the button to view all the commands"))
                .AddComponents(basicsBtn, calcBtn);

            await ctx.Channel.SendMessageAsync(msg);
        }
    }
}
