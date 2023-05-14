using g2v.core.clinetsync.api.Controllers;
using Hangfire;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Net;
using System.Threading;
using System.Timers;

namespace g2v.core.clinetsync.api
{
    public class ChatHub : Hub
    {
        private System.Threading.Timer? timer;
        private readonly IHubContext<ChatHub> _context;
        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _context = hubContext;
        }
        public async Task SendMessage(string user, string message)
        {

            timer = new System.Threading.Timer(DoWork, null, TimeSpan.FromSeconds(3),
                 TimeSpan.FromSeconds(1));
            await Clients.All.SendAsync("ReceiveMessage", user,timer.GetType().Name);
        }

        private void DoWork(object? state)
        {
            
            _context.Clients.All.SendAsync("ReceiveMessage", DateTime.Now.Ticks, DateTime.Now.Ticks);
        }
    }
}
