using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FTsR.Hubs;
public class NotificationHub : Hub
{
    public async Task SendMessage(string username, string message)
    {
        await Clients.Others.SendAsync("ReceiveMessage", username, message);
    }
}

