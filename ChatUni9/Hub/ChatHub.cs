using Castle.Core.Logging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatUni9.ChatHub
{
    public class ChatHub: Hub
    {

        public ILogger Logger { get; set; }

        public ChatHub()
        {
            Logger = NullLogger.Instance;
        }

        public async Task SendMessage(string user ,string message)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Logger.Debug("A client connected to MyChatHub: " + Context.UserIdentifier);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            Logger.Debug("A client disconnected from MyChatHub: " + Context.UserIdentifier);
        }
    }
}
