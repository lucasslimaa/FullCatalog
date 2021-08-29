using FullCatalog.Business.Interfaces;
using KissLog;
using System.Collections.Generic;
using System.Linq;

namespace FullCatalog.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;
        private readonly ILogger _logger;
        public Notifier(ILogger logger)
        {
            _notifications = new List<Notification>();
            _logger = logger;
        }
        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
            _logger.Warn(notification.Message);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }

}
