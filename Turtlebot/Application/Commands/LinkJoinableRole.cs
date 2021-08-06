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
    public class LinkJoinableRole : ICommand {

        private readonly MessageHandler handler;
        private readonly Context ctx;

        public LinkJoinableRole(MessageHandler handler, Context ctx) {
            this.handler = handler;
            this.ctx = ctx;
        }
        public async Task Execute(MessageCreateEventArgs e)
        {
            var admin = this.ctx.Admins.Where(x => x.DiscordID == e.Author.Id.ToString()).FirstOrDefault();
            if (admin == null) {
                e.Message.RespondAsync("You do not have the permissions to do this");
                return;
            }

            List<string> args = this.handler.GetArgs(e.Message.Content);
            var aliasCheck = this.ctx.JoinableRoles.Where(x => x.Alias == args[0]).FirstOrDefault();
            if(aliasCheck != null){
                await e.Message.RespondAsync("Alias already exists");
                return;
            }

            var roleCheck = e.Guild.Roles.Where(x => x.Id.ToString() == args[1]).FirstOrDefault();
            if(roleCheck == null){
                await e.Message.RespondAsync("Role not found");
                return;
            }

            JoinableRole role = new JoinableRole();
            role.ID = Guid.NewGuid();
            role.RoleID = args[1];
            role.Alias = args[0];
            ctx.JoinableRoles.Add(role);
            
            ctx.SaveChanges();

            await e.Message.RespondAsync("Role linked");
            return;
        }

        public string GetEntry()
        {
            return "link-role";
        }
    }
}