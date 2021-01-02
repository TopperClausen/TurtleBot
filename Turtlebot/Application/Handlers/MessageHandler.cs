using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Application.Handlers
{
    public class MessageHandler
    {

        List<ICommand> commands;

        private Client client;

        public MessageHandler(Client parrent)
        {
            this.client = parrent;
            ListCommands();
        }

        private void ListCommands()
        {
            this.commands = new List<ICommand>();
            commands.Add(new GetRandomQuote());
        }

        public async Task OnMessage(MessageCreateEventArgs e)
        {
            Console.WriteLine("Received message " + e.Message.Content);

            if (e.Message.Author.IsBot || !e.Message.Content.StartsWith('!'))
                return;
            
            string commandFired = GetCommand(e.Message.Content);
            Console.WriteLine(commandFired);
            foreach(ICommand item in commands)
            {
                if(commandFired == item.GetEntry())
                {
                    await item.Execute(e);
                    break;
                }
            }
        }

        public List<string> GetArgs(string msg)
        {
            string[] splitMessage = msg.Split(' ');
            List<string> args = new List<string>();
            for (int i = 0; i < splitMessage.Length - 1; i++){
                args.Add(splitMessage[i]);
            }
            return args;
        }

        public string GetCommand(string msg)
        {
            string[] splitMessage = msg.Split(' ');
            return splitMessage[0].Remove('!');
        }
    }
}
