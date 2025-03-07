using Microsoft.AspNetCore.SignalR;

namespace TaskManagementAPI.Hubs 
{
    public class TaskHub : Hub
    {
        public async Task SendMessage(string title, string? description, string status)
        {
            await Clients.All.SendAsync("RecieveMessage", title, description, status);
        }
    }
}