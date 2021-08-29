using FluentValidation;

namespace FullCatalog.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("The field {PropertyName} must have a value")
                .Length(2, 200).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Neighborhood)
                .NotEmpty().WithMessage("The field {PropertyName} must have a value")
                .Length(2, 100).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("The field {PropertyName} must have a value")
                .Length(8).WithMessage("O field {PropertyName} precisa ter {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("A field {PropertyName} precisa ser fornecida")
                .Length(2, 100).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("The field {PropertyName} must have a value")
                .Length(2, 50).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("The field {PropertyName} must have a value")
                .Length(1, 50).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");
        }
    }
}
