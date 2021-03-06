using FullCatalog.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullCatalog.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool OperationIsValid()
        {
            return !_notifier.HasNotification();
        }
    }
}
