using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.Features.Users.GetPlayerProfile;

public class GetPlayerProfileHandler : IRequestHandler<GetPlayerProfileQuery, IResponse<GetPlayerProfileResponse>>
{
    private readonly IDiplomaChatContext _diplomaChatContext;

    public GetPlayerProfileHandler(IDiplomaChatContext diplomaChatContext)
    {
        _diplomaChatContext = diplomaChatContext;
    }

    public async Task<IResponse<GetPlayerProfileResponse>> Handle(
        GetPlayerProfileQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _diplomaChatContext.EntitySet<User>()
            .TopOneAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return new Response<GetPlayerProfileResponse>
            {
                Status = ResponseStatus.Conflict
            };
        }

        var response = new GetPlayerProfileResponse
        {
            Nickname = user.Nickname
        };

        return response.Success();
    }
}