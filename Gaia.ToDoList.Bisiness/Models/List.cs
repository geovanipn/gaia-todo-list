using System.Collections.Generic;

namespace Gaia.ToDoList.Business.Models
{
    public sealed class List : Entity
    {
        private readonly List<Item> _items;

        public string Title { get; private set; }

        public long UserId { get; private set; }

        public User User { get; private set; }

        public IReadOnlyCollection<Item> Items => _items;

        private List()
        {
            _items = new List<Item>();
        }

        public static List Create(in string title, in long UserId) =>
            new List { UserId = UserId, Title = title };

        public Item Add(Item item)
        {
            item.ListId = Id;
            
            _items.Add(item);
            
            return item;
        }
    }
}
