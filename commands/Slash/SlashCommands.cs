using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Discord_App.commands.Slash
{
    public class SlashCommands : ApplicationCommandModule
    {
        [SlashCommand("chao", "Bam thu xem")]
        public async Task FirstSlashCommand(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            var embedMsg = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Aquamarine,
                Title = "Xin chao"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMsg));
        }
    }
}
