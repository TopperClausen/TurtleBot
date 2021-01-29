using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Handlers;
using Application.Models;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Application.Commands {
    public class JoinRole : ICommand {

        private readonly MessageHandler handler;

        public JoinRole(MessageHandler handler) {
            this.handler = handler;
        }
        public async Task Execute(MessageCreateEventArgs e)
        {
            List<string> args = this.handler.GetArgs(e.Message.Content);
            var ctx = new Context();
            JoinableRole dbrole = ctx.JoinableRoles.Where(x => x.Alias == args[0]).FirstOrDefault();

            var role = e.Guild.Roles.Where(x => x.Id.ToString() == dbrole.RoleID).FirstOrDefault();
            var member = (DiscordMember) e.Message.Author;
            await member.GrantRoleAsync(role);
            await e.Message.RespondAsync("Role granted");
        }

        public string GetEntry()
        {
            return "join";
        }
    }
}