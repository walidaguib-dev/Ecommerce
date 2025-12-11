using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;


namespace Ecommerce.Hubs
{
    [SignalRHub]
    public class MessagesHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            System.Console.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }


        [SignalRMethod(name: "sending message")]
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}