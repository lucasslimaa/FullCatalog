using FluentValidation;
using FluentValidation.Results;
using FullCatalog.Business.Interfaces;
using FullCatalog.Business.Notifications;

namespace FullCatalog.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notofier;

        public BaseService(INotifier notifier)
        {
            _notofier = notifier;
        }
        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string message)
        {
            _notofier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;

        }
    }
}
