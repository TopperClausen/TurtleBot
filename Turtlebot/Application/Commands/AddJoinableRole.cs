using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Handlers;
using Application.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Application.Commands {
    public class AddJoinableRole : ICommand {

        private readonly MessageHandler handler;

        public AddJoinableRole(MessageHandler handler) {
            this.handler = handler;
        }
        public async Task Execute(MessageCreateEventArgs e)
        {
            Context ctx = new Context();
            Admin admin = ctx.Admins.Where(x => x.DiscordID == e.Message.Author.Id.ToString()).FirstOrDefault();
            if(admin == null)
                return;

            List<string> args = handler.GetArgs(e.Message.Content);
            var check = e.Guild.Roles.Where(x => x.Name == args[1]).FirstOrDefault();
            if(check != null)
            {
                e.Message.RespondAsync("Role already exists");
                return;
            }

            var role = await e.Guild.CreateRoleAsync(args[1], null, DiscordColor.CornflowerBlue, false, true);
            JoinableRole dbrole = new JoinableRole();
            dbrole.Alias = args[0];
            dbrole.RoleID = role.Id.ToString();
            dbrole.ID = Guid.NewGuid();

            ctx.JoinableRoles.Add(dbrole);
            ctx.SaveChanges();
            e.Message.RespondAsync("Role created");
        }

        public string GetEntry()
        {
            return "add-role";
        }
    }
}