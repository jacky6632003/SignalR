using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrCore.Infrastructure.SignalR
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> OnLineUsers = new ConcurrentDictionary<string, string>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Desktop登入
        /// </summary>
        /// <param name="name">The name.</param>
        public async Task Login(string name)
        {
            OnLineUsers.AddOrUpdate(Context.ConnectionId, name, (key, value) => name);
            await Clients.AllExcept(Context.ConnectionId).
                SendAsync("online", $"{name} 進入群組");
        }

        /// <summary>
        /// Desktop離開
        /// </summary>
        /// <param name="name">The name.</param>
        public async Task SignOut(string name)
        {
            await Clients.AllExcept(Context.ConnectionId)
                .SendAsync("online", $"{name} 離開群組." +
                $"！");
        }

        public async Task SendMessageByServer(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, "系統通知:" + message);
        }

        public async Task SendOneMessage(string toUserId, string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessageOne", @$"您對{user}說", message);
            await Clients.Client(toUserId).SendAsync("ReceiveMessageOne", user + "對您", message);
        }

        public override System.Threading.Tasks.Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}