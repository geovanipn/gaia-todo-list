
namespace Gaia.ToDoList.Api.InputModels
{
    public class UpdateUserInputModel
    {
        public long Id { get; set; }

        public UserInputModel UserInputModel{ get; set; }

        public UpdateUserInputModel(long id, UserInputModel userInputModel)
        {
            Id = id;
            UserInputModel = userInputModel;
        }
    }
}
