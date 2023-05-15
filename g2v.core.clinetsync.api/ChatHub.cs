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
        private readonly IBackgroundJobClient _jobClient;
        public ChatHub(IHubContext<ChatHub> hubContext, IBackgroundJobClient jobClient)
        {
            _context = hubContext;
            _jobClient = jobClient;
        }
        public async Task SendMessage(string user, string message)
        {

            timer = new System.Threading.Timer(DoWork, null, TimeSpan.FromSeconds(3),
                 TimeSpan.FromSeconds(1));

            _jobClient.Schedule(() =>  GetCurrentUserNotifications(),   TimeSpan.FromSeconds(2));

            RecurringJob.AddOrUpdate("chartsjob",     () => GetCurrentUserNotifications(),     Cron.Minutely);

            await Clients.All.SendAsync("ReceiveMessage", user,timer.GetType().Name);
        }

        private void DoWork(object? state)
        {
            Console.WriteLine("aaa");
            GetCurrentUserNotifications();
            //_context.Clients.All.SendAsync("ReceiveMessage", DateTime.Now.Ticks, DateTime.Now.Ticks);
        }

        public void GetCurrentUserNotifications()
        {
            int[] arr = getArray();
            _context.Clients.All.SendAsync("ReceiveMessage", arr);
        }

        private static int[] getArray()
        {
            int Min = 0;
            int Max = 20;
            Random randNum = new Random();
            int[] arr = Enumerable
                .Repeat(0, 100)
                .Select(i => randNum.Next(Min, Max))
                .ToArray();
            return arr;
        }
    }
}
