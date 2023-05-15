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

        private readonly IHubContext<ChatHub> _context;

        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _context = hubContext;
            
        }
        public async Task SendMessage(string user, string message)
        {
            RecurringJob.AddOrUpdate("chartsjob", ()=> GetCurrentUserNotifications(),Cron.Minutely);

            await Clients.All.SendAsync("ReceiveMessage2", user,message);
        }
        public void GetCurrentUserNotifications()
        {
            int[] arr = GetArray();
            _context.Clients.All.SendAsync("ReceiveMessage", arr);
        }

        private static int[] GetArray()
        {
            int Min = 0;
            int Max = 20;
            var randNum = new Random();
            int[] arr = Enumerable
                .Repeat(0, 100)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();
            return arr;
        }
    }
}
