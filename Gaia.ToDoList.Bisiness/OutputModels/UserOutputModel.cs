using System.Collections.Generic;

namespace Gaia.ToDoList.Business.OutputModels
{
    public sealed class UserOutputModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public ICollection<ListOutputModel> Lists { get; set; }

        public ICollection<ItemOutputModel> Items { get; set; }
    }
}
