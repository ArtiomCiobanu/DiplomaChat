using System;
using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.Features.Users.RegisterPlayer
{
    public class RegisterPlayerCommand : ICommand
    {
        public Guid PlayerId { get; set; }
        public string PlayerNickname { get; set; }
    }
}