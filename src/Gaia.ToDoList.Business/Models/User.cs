using System.Collections.Generic;

namespace Gaia.ToDoList.Business.Models
{
    public sealed class User : Entity
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public ICollection<List> Lists { get; private set; }

        public ICollection<Item> Items { get; private set; }

        private User() { }

        public static User Create(
            in string name, 
            in string email, 
            in string login, 
            in string password,
            in long id = default) =>
            new User
            {
                Id = id,
                Name = name,
                Email = email,
                Login = login,
                Password = password
            };   
    }
}
