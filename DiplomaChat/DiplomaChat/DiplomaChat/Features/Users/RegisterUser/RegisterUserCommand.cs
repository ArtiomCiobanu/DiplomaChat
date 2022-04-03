using System;
using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.Features.Users.RegisterUser
{
    public class RegisterUserCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public string PlayerNickname { get; set; }
    }
}