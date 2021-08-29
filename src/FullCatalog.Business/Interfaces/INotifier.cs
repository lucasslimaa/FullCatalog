using System;
using System.Collections.Generic;
using FullCatalog.Business.Notifications;

namespace FullCatalog.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
