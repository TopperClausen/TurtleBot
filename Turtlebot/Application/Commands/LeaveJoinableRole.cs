using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Handlers;
using Application.Models;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading;

namespace Application.Commands {
    public class LeaveJoinableRole : ICommand {

        private MessageHandler handler;
        private Context ctx;
        public LeaveJoinableRole(MessageHandler handler, Context ctx) {
            this.handler = handler;
            this.ctx = ctx;
        }
        public async Task Execute(MessageCreateEventArgs e)
        {
            List<string> args = this.handler.GetArgs(e.Message.Content);
            if(args.Count() == 0){
                await e.Message.RespondAsync("command: !leave <alias>");
                return;
            }

            JoinableRole dbRole = ctx.JoinableRoles.Where(r => r.Alias == args[0]).FirstOrDefault();
            if(dbRole == null) {
                await e.Message.RespondAsync("alias not found");
                return;
            }

            var role = e.Guild.Roles.Where(x => x.Id.ToString() == dbRole.RoleID).FirstOrDefault();
            if(role == null){
                await e.Message.RespondAsync("Role not found, contact Topper");
                return;
            }

            DiscordMember member = (DiscordMember) e.Message.Author;
            await member.RevokeRoleAsync(role, "COMMAND ISSUED");
            var response = await e.Message.RespondAsync("Role revoked");

            e.Message.DeleteAsync();
            Thread.Sleep(5000);
            await response.DeleteAsync();
            return;
        }

        public string GetEntry()
        {
            return "leave";
        }
    }
}