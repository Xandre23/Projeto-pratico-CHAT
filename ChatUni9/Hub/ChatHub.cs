using Castle.Core.Logging;
using ChatUni9.DAO.Talk;
using ChatUni9.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatUni9.ChatHub
{
    public class ChatHub: Hub
    {
        const TalkDAO talkDAO = new TalkDAO();
        public ILogger Logger { get; set; }

        public ChatHub()
        {
            Logger = NullLogger.Instance;
        }

        public async Task SendMessage(string user ,string message)
        {
            talkDAO.InsetMessage(new TalkViewModel());
            await Clients.User(user).SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            ConnectedUserViewModel.Ids.Add(Context.UserIdentifier);
            Logger.Debug("A client connected to MyChatHub: " + Context.UserIdentifier);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUserViewModel.Ids.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
            Logger.Debug("A client disconnected from MyChatHub: " + Context.UserIdentifier);
        }
    }
}
