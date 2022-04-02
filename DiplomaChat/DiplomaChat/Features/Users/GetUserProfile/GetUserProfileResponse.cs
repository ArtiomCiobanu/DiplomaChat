using System;

namespace DiplomaChat.Features.Users.GetUserProfile
{
    public class GetUserProfileResponse
    {
        public Guid UserId { get; init; }

        public string Nickname { get; init; }
    }
}
