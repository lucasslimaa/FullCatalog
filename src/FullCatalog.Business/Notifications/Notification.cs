using System;
using System.Text;
using System.Threading.Tasks;

namespace FullCatalog.Business.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }

}
