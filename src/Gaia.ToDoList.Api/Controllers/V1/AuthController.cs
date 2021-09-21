using System.Threading.Tasks;
using Gaia.ToDoList.Api.InputModels;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.ToDoList.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public sealed class AuthController : ApiController
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthController(INotifier notifier, IAuthenticateService authenticateService)
            : base(notifier)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<AuthorizationOutputModel>> Authenticate([FromBody] AuthenticateInputModel authenticateInputModel)
        {
            var authorization = await _authenticateService
                .Authenticate(authenticateInputModel.Login, authenticateInputModel.Password);

            return Response(authorization);
        }
    }
}
