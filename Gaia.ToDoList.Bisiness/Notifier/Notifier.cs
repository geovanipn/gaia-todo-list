using System.Collections.Generic;
using Gaia.ToDoList.Business.Interfaces.Notifier;
using Microsoft.EntityFrameworkCore.Internal;

namespace Gaia.ToDoList.Business.Notifier
{
    public sealed class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }
    }
}
