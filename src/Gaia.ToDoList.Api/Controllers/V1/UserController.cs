using System.Threading.Tasks;
using AutoMapper;
using Gaia.Helpers.AppSettings;
using Gaia.Helpers.Security;
using Gaia.ToDoList.Api.InputModels;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;
using Gaia.ToDoList.Business.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.ToDoList.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public sealed class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IUserService userService,
            INotifier notifier,
            IMapper mapper) : base(notifier)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<UserOutputModel>> GetById(long id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Response(_mapper.Map<UserOutputModel>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserOutputModel>> CreateUser([FromBody]UserInputModel userInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userInputModel.Password = EncryptPassword(userInputModel.Password);
            
            var user = await _userService.Create(_mapper.Map<User>(userInputModel));

            return Response(_mapper.Map<UserOutputModel>(user));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<UserOutputModel>> UpdateUser([FromBody]UserInputModel userInputModel, [FromRoute]long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            userInputModel.Password = EncryptPassword(userInputModel.Password);
            
            var user = await _userService.Update(_mapper.Map<User>(new UpdateUserInputModel(id, userInputModel)));

            return Response(_mapper.Map<UserOutputModel>(user));
        }

        private static string EncryptPassword(string password)
        {
            return Encryption.Encrypt(password,
                AppSettingsHelper.GetValue<string>("Encryption:PassPhrase"));
        }
    }
}
