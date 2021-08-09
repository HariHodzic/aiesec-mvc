using Microsoft.AspNetCore.SignalR;

namespace Aiesec.Web.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}