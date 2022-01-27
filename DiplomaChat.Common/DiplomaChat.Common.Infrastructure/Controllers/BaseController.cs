using DiplomaChat.Common.Infrastructure.ResponseMappers;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Common.Infrastructure.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IResponseMapper ResponseMapper { get; }

        public BaseController(IResponseMapper responseMapper)
        {
            ResponseMapper = responseMapper;
        }
    }
}
