using System.Collections.Generic;
using Gaia.ToDoList.Business.Notifier;

namespace Gaia.ToDoList.Business.Interfaces.Notifier
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}
