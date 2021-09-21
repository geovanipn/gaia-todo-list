
namespace Gaia.ToDoList.Business.OutputModels
{
    public sealed class AuthorizationOutputModel
    {
        public UserOutputModel User { get; set; }

        public string Token { get; set; }
    }
}
