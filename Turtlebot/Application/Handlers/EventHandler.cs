using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Application.Handlers
{
    public class EventHandler
    {
        public Client client;
        public MessageHandler msgHandler;

        public EventHandler(Client parrent)
        {
            this.client = parrent;
            this.msgHandler = new MessageHandler(this);
        }

        public Task Handle()
        {
            this.client.client.MessageCreated += msgHandler.OnMessage;

            return Task.CompletedTask;
        }

        
    }
}
