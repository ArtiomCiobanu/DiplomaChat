using MediatR;

namespace DiplomaChat.Common.Infrastructure
{
    public interface ICommand : IQuery<Unit>
    {
    }
}
