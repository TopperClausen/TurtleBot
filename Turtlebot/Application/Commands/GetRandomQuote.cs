using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Application.Commands
{
    public class GetRandomQuote : ICommand
    {
        public async Task Execute(MessageCreateEventArgs e)
        {
            var channel = e.Guild.Channels.Where(c => c.Id == 208560106701455360).FirstOrDefault();
            if (channel == null)
            {
                await e.Channel.SendMessageAsync("Channel not found");
                return;
            }
            var messages = await channel.GetMessagesAsync();

            var r = new Random();
            int rInt = r.Next(0, messages.Count - 1);

            var Builder = new DiscordEmbedBuilder();
            Builder.Description = messages[rInt].Content;
            Builder.Title = "Quote";
            Builder.Color = DiscordColor.Orange;

            var Embed = Builder.Build();
            await e.Message.RespondAsync(null, false, Embed);

            await e.Message.DeleteAsync();
        }

        public string GetEntry()
        {
            return "q";
        }
    }
}
