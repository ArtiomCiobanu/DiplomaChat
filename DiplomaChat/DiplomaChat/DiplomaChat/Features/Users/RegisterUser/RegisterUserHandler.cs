using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IResponse<Unit>>
    {
        private readonly IDiplomaChatContext _diplomaChatContext;

        public RegisterUserHandler(IDiplomaChatContext diplomaChatContext)
        {
            _diplomaChatContext = diplomaChatContext;
        }

        public async Task<IResponse<Unit>> Handle(
            RegisterUserCommand request,
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