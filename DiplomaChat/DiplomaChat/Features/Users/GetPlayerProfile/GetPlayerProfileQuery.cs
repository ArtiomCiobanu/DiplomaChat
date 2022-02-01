using System;
using DiplomaChat.Common.Infrastructure;

namespace TileGameServer.Features.Users.GetPlayerProfile;

public class GetPlayerProfileQuery : IQuery<GetPlayerProfileResponse>
{
    public Guid UserId { get; init; }
}