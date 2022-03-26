using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Domain.Entities;

namespace TileGameServer.InSession.Hubs
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
            //For testing purposes
            /*var tileField = new TileField(
                new Tile(),
                new TilePosition { X = 50, Y = 50 },
                new FieldSize { Height = 100, Width = 100 });

            var gameSession = new GameSession(tileField)
            {
                Id = new Guid("231d304e-fb83-4738-8507-30807ad400b8")
            };
            var player = new SessionPlayer
            {
                Id = new Guid("321d304e-fb83-4738-8507-30807ad400a7"),
                Nickname = "Nickname!",
                GameSession = gameSession
            };
            gameSession.Players.Add(player);
            _inSessionContext.EntitySet<SessionPlayer>().Add(player);
            _inSessionContext.EntitySet<GameSession>().Add(gameSession);*/

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

            var sessionPlayer = _inSessionContext.EntitySet<ChatMember>()
                .FirstOrDefault(p => p.Id == userId);

            if (sessionPlayer != null)
            {
                await Clients.All.SendAsync("PlayerConnected", sessionPlayer.Nickname);
            }
        }

        public async Task Disconnect(Guid userId)
        {
            var users = _inSessionContext.EntitySet<ChatMember>();
            var sessionUser = users.FirstOrDefault(p => p.Id == userId);

            if (sessionUser == null)
            {
                return;
            }

            users.Remove(sessionUser);

            var gameSession = _inSessionContext.EntitySet<ChatRoom>()
                .FirstOrDefault(gs => gs.ChatMembers.Any(p => p.Id == userId));
            if (gameSession != null)
            {
                var userInSession = gameSession.ChatMembers.FirstOrDefault(p => p.Id == userId);

                gameSession.ChatMembers.Remove(userInSession);
            }

            await Clients.All.SendAsync("PlayerDisconnected", sessionUser.Nickname);
        }

        public Task SendMessage(
            Guid userId,
            string message)
        {
            return null;
        }
    }
}