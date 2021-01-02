using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DSharpPlus.Entities;
using Application.Handlers;

namespace Application
{
    public class Client
    {
        public Handlers.MessageHandler msgHandler;
        public DiscordClient client = new DiscordClient(new DiscordConfiguration {
            Token = "NzczNjYxODY5MDM4MTA4Njgy.X6Meww.G9mFoXdu6qJDSbl4CJ0jhkHuo9A",
            TokenType = TokenType.Bot
        });

        public async Task Init(string[] args)
        {
            try
            {
                await client.ConnectAsync();
                Console.WriteLine("Connected to Discord");
                this.msgHandler = new Handlers.MessageHandler(this);
            }catch(Exception e)
            {
                Console.WriteLine("Could not connect to discord");
                Console.WriteLine(e.Message.ToString());
                return;
            }

            this.client.MessageCreated += msgHandler.OnMessage;

            await Task.Delay(-1);
        }
    }
}
