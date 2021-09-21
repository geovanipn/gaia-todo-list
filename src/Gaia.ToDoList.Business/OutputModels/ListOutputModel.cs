using System.Collections.Generic;

namespace Gaia.ToDoList.Business.OutputModels
{
    public sealed class ListOutputModel
    {
        public long UserId { get; set; }

        public long Id { get; set; }

        public string Title { get; set; }

        public ICollection<ItemOutputModel> Items { get; set; }
    }
}
