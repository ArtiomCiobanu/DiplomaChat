using System;
using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.Features.Users.GetUserProfile;

public class GetUserProfileQuery : IQuery<GetUserProfileResponse>
{
    public Guid UserId { get; init; }
}