using AutoMapper;
using Gaia.ToDoList.Api.InputModels;
using Gaia.ToDoList.Business.Models;
using Gaia.ToDoList.Business.OutputModels;

namespace Gaia.ToDoList.Api.Configuration.AutoMapper
{
    public sealed class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AuthenticateInputModel, User>().ReverseMap();
            CreateMap<User, UserOutputModel>();

            CreateMap<UserInputModel, User>()
                .ConstructUsing(x => 
                    User.Create(x.Name, x.Email, x.Login, x.Password, default));

            CreateMap<UpdateUserInputModel, User>()
                .ConstructUsing(x => 
                    User.Create(
                        x.UserInputModel.Name, 
                        x.UserInputModel.Email, 
                        x.UserInputModel.Login, 
                        x.UserInputModel.Password, 
                        x.Id));

            CreateMap<ListInputModel, List>()
                .ConstructUsing((listInputModel, resolveContext) =>
                    List.Create(listInputModel.Title, (long)resolveContext.Items[nameof(List.UserId)]));

            CreateMap<List, ListOutputModel>();

            CreateMap<Item, ItemOutputModel>();

            CreateMap<ItemInputModel, Item>()
                .ConstructUsing((listInputModel, resolveContext) =>
                    Item.Create(listInputModel.Title, 
                        listInputModel.Description, 
                        (long)resolveContext.Items[nameof(List.UserId)]));
        }
    }
}
