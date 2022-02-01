using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using MediatR;
using TileGameServer.DataAccess.Context;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.Features.Users.RegisterPlayer
{
    public class RegisterPlayerHandler : IRequestHandler<RegisterPlayerCommand, IResponse<Unit>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public RegisterPlayerHandler(IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<Unit>> Handle(
            RegisterPlayerCommand request,
            CancellationToken cancellationToken)
        {
            var userExists = await _diplomaChatContext.EntitySet<User>()
                .ExistsAsync(u => u.Id == request.PlayerId, cancellationToken);

            if (userExists)
            {
                return new Unit().Forbidden();
            }

            var player = new User
            {
                Id = request.PlayerId,
                Nickname = request.PlayerNickname
            };

            await _diplomaChatContext.EntitySet<User>().AddAsync(player, cancellationToken);
            await _diplomaChatContext.SaveChangesAsync(cancellationToken);

            return new Unit().Success();
        }
    }
}