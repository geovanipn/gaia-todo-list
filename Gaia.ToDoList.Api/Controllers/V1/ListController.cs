using System.Threading.Tasks;
using AutoMapper;
using Gaia.ToDoList.Api.InputModels;
using Gaia.ToDoList.Business.Interfaces.AppUser;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Gaia.ToDoList.Business.Interfaces.Repositories;
using Gaia.ToDoList.Business.Interfaces.Services;
using Gaia.ToDoList.Business.Models;
using Gaia.ToDoList.Business.OutputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.ToDoList.Api.Controllers.V1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/lists")]
    public sealed class ListController : ApiController
    {
        private readonly IListService _listService;
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;


        public ListController(
            INotifier notifier, 
            IListService listService,
            IListRepository listRepository,
            IMapper mapper,
            IAuthenticatedUser authenticatedUser) : base(notifier)
        {
            _listService = listService;
            _listRepository = listRepository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ListOutputModel>> GetById(long id)
        {
            var list = await _listRepository.GetById(id);
            if (list == null)
            {
                return NotFound();
            }

            return Response(_mapper.Map<ListOutputModel>(list));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ListOutputModel>> Create([FromBody] ListInputModel listInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _authenticatedUser.GetUserId();

            var list = _mapper.Map<List>(
                listInputModel, 
                options => options.Items.Add(nameof(List.UserId), userId));
            
            await _listService.Create(list);

            return Response(_mapper.Map<ListOutputModel>(list));
        }

        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var list = await _listRepository.GetById(id);
            if (list == null)
            {
                return NotFound();
            }

            var userId = _authenticatedUser.GetUserId();
            if (list.UserId != userId)
            {
                return Unauthorized();
            }

            await _listService.Delete(list);
            return Response(list.Id);
        }
    }
}
