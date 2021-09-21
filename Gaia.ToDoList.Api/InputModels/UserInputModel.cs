using System.ComponentModel.DataAnnotations;

namespace Gaia.ToDoList.Api.InputModels
{
    public class UserInputModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Password { get; set; }
    }
}
