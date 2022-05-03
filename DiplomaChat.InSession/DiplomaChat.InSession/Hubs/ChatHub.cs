using DiplomaChat.InSession.DataAccess.Contracts.Context;
using DiplomaChat.InSession.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace DiplomaChat.InSession.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IInSessionContext _inSessionContext;

        public ChatHub(
            IInSessionContext inSessionContext)
        {
            _inSessionContext = inSessionContext;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Connect(Guid userId)
        {
            if (userId == default)
            {
                return;
            }

            var chatMember = _inSessionContext.EntitySet<ChatMember>()
                .FirstOrDefault(p => p.Id == userId);

            if (chatMember != null)
            {
                await Clients.All.SendAsync("UserConnected", userId, chatMember.Nickname);
            }
        }

        public async Task Disconnect(Guid userId)
        {
            var users = _inSessionContext.EntitySet<ChatMember>();
            var sessionMember = users.FirstOrDefault(p => p.Id == userId);

            if (sessionMember == null)
            {
                return;
            }

            users.Remove(sessionMember);

            var chatRoom = _inSessionContext.EntitySet<ChatRoom>()
                .FirstOrDefault(gs => gs.ChatMembers.Any(p => p.Id == userId));
            if (chatRoom != null)
            {
                var userInSession = chatRoom.ChatMembers.FirstOrDefault(p => p.Id == userId);

                chatRoom.ChatMembers.Remove(userInSession);
            }

            await Clients.All.SendAsync("UserDisconnected", sessionMember.Nickname);
        }

        public async Task SendMessage(
            Guid userId,
            string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}