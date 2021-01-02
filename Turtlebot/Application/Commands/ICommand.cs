using System;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;

namespace Application.Commands
{
    public interface ICommand
    {
        public string GetEntry();
        public Task Execute(MessageCreateEventArgs e);
    }
}
