using Hangfire;
using Microsoft.AspNetCore.SignalR;
namespace g2v.core.clinetsync.notifications
{
    public class NotificationHub : Hub
    {

        private readonly IHubContext<NotificationHub> _context;

        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _context = hubContext;

        }
        public async Task SendMessage(string user, string message)
        {
            //schedules a job every 1 seconds
            RecurringJob.AddOrUpdate("chartsjob", () => GetCurrentUserNotifications(), "*/1 * * * * *");

            await Clients.All.SendAsync("ReceiveMessage2", user, message);
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