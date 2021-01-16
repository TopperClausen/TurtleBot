using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Application.Handlers {
    public class EventHandler {

        public async Task OnGuildMemberJoin(GuildMemberAddEventArgs args) {
            var role = args.Guild.Roles.Where(r => r.Name.ToUpper() == "MUTANT TURTLE").FirstOrDefault();
            args.Member.GrantRoleAsync(role, "member joined");
        }

    }
}