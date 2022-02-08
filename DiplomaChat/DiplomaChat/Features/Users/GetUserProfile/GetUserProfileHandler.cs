using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Users.GetUserProfile;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, IResponse<GetUserProfileResponse>>
{
    private readonly IDiplomaChatContext _diplomaChatContext;

    public GetUserProfileHandler(IDiplomaChatContext diplomaChatContext)
    {
        _diplomaChatContext = diplomaChatContext;
    }

    public async Task<IResponse<GetUserProfileResponse>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _diplomaChatContext.EntitySet<User>()
            .TopOneAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return new Response<GetUserProfileResponse>
            {
                Status = ResponseStatus.Conflict
            };
        }

        var response = new GetUserProfileResponse
        {
            Nickname = user.Nickname
        };

        return response.Success();
    }
}