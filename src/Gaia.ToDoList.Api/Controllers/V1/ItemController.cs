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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/lists/{listId:long}/items")]
    public sealed class ItemController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IListRepository _listRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;
        private readonly IAuthenticatedUser _authenticatedUser;

        public ItemController(
            INotifier notifier,
            IMapper mapper,
            IListRepository listRepository,
            IItemRepository itemRepository,
            IItemService itemService, 
            IAuthenticatedUser authenticatedUser) : base(notifier)
        {
            _mapper = mapper;
            _listRepository = listRepository;
            _itemRepository = itemRepository;
            _itemService = itemService;
            _authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult<ItemOutputModel[]>> GetItems(long listId)
        {
            var items = await _itemRepository.GetByListId(listId);
            
            return Response(_mapper.Map<ItemOutputModel[]>(items));
        }

        
        [HttpPost]
        public async Task<ActionResult<ItemOutputModel>> Add([FromBody]ItemInputModel itemInputModel, long listId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = await _listRepository.GetById(listId);
            if (list == null)
            {
                return NotFound();
            }

            var userId = _authenticatedUser.GetUserId();
            var item = _mapper.Map<Item>(
                itemInputModel,
                options => options.Items.Add(nameof(List.UserId), userId));

            list.Add(item);
            
            await _itemService.Create(item);

            return Response(_mapper.Map<ItemOutputModel>(item));
        }
    }
}
