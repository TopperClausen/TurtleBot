using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Handlers;
using Application.Models;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading;

namespace Application.Commands {
    public class JoinRole : ICommand {

        private readonly MessageHandler handler;
        private Context ctx;

        public JoinRole(MessageHandler handler, Context ctx) {
            this.handler = handler;
            this.ctx = ctx;
        }
        public async Task Execute(MessageCreateEventArgs e)
        {
            List<string> args = this.handler.GetArgs(e.Message.Content);
            JoinableRole dbrole = this.ctx.JoinableRoles.Where(x => x.Alias == args[0]).FirstOrDefault();

            var role = e.Guild.Roles.Where(x => x.Id.ToString() == dbrole.RoleID).FirstOrDefault();
            var member = (DiscordMember) e.Message.Author;
            await member.GrantRoleAsync(role);
            var response = await e.Message.RespondAsync("Role granted");

            await e.Message.DeleteAsync();

            Thread.Sleep(5000);
            await response.DeleteAsync();
        }

        public string GetEntry()
        {
            return "join";
        }
    }
}