using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatR
{
    public class NotificationHub : Hub
    {
        ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
            _logger.LogInformation("NotificationHub Created");
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("New Connection from Client");
            await base.OnConnectedAsync();
            await ServerMessage("Welcome to ChatR");
        }

        public async Task Broadcast(string from, string message)
        {
            _logger.LogInformation($"{from} broadcasts message: {message}");
            await Clients.All.SendAsync("BroadcastChannel", from, message);
        }

        public async Task ServerMessage(string message)
        {
            _logger.LogInformation($"Server sends message: {message}");
            await Clients.Caller.SendAsync("ServerChannel", message);
        }
    }
}