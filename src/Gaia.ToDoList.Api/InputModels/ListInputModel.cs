using System.ComponentModel.DataAnnotations;

namespace Gaia.ToDoList.Api.InputModels
{
    public sealed class ListInputModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string Title { get; set; }
    }
}
