using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ArchitectureProject.Infrastructure.SignalR.Hubs
{
    public class DefaultHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await this.Clients.Others.SendAsync("UserIsLogged",$"User {Context.User.Identity.Name} connected");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.Others.SendAsync("UserIsDisconnected", $"User {Context.User.Identity.Name} disconnected");
        }
    }
}
