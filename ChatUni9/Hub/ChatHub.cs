﻿using Castle.Core.Logging;
using ChatUni9.DAO.Talk;
using ChatUni9.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatUni9.ChatHub
{
    public class ChatHub: Hub
    {
        TalkDAO talkDAO = new TalkDAO();
        public ILogger Logger { get; set; }

        public ChatHub()
        {
            Logger = NullLogger.Instance;
        }

        public async Task SendMessage(int user, string message)
        {
            var userIsLoggedIn = ConnectedUserViewModel.Ids.Contains(user.ToString());
            var talkViewModel = new TalkViewModel();
            talkViewModel.IDUserIssuer = Convert.ToInt32(Context.UserIdentifier);
            talkViewModel.IDUserReceiver = user;
            talkViewModel.Message = message;
            talkViewModel.DateTime = DateTime.Now;            

            if (userIsLoggedIn)
            {
                talkViewModel.Visualized = true;
                var messageObject = new
                {
                    receiver = user,
                    issuer = talkViewModel.IDUserIssuer,
                    message = message
                };

                await Clients.User(user.ToString()).SendAsync("ReceiveMessage", user, messageObject);
            }
            else
            {
                talkViewModel.Visualized = false;                
            }
            await talkDAO.InsetMessage(talkViewModel);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            ConnectedUserViewModel.Ids.Add(Context.UserIdentifier);
            await Clients.All.SendAsync("NewUserOnline", Context.UserIdentifier);
            Logger.Debug("A client connected to MyChatHub: " + Context.UserIdentifier);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUserViewModel.Ids.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
            await Clients.All.SendAsync("AUserHasLoggedOut", Context.UserIdentifier);
            Logger.Debug("A client disconnected from MyChatHub: " + Context.UserIdentifier);
        }
    }
}
