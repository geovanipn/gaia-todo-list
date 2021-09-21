
namespace Gaia.ToDoList.Business.Models
{
    public sealed class Item : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public long UserId { get; set; }

        public long ListId { get; set; }
        
        public User User { get; set; }
        
        public List List { get; set; }

        private Item() { }

        public static Item Create(in string title, in string description, in long userId) =>
            new Item { UserId = userId, Title = title, Description = description};
    }
}
