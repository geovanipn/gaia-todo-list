
namespace Gaia.ToDoList.Business.Notifier
{
    public sealed class Notification
    {
        public string Message { get; }

        public Notification(string message)
        {
            Message = message;
        }
    }
}
