using FluentValidation;
using FullCatalog.Business.Models.Validations.Documents;

namespace FullCatalog.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("The field {PropertyName} cannot be empty")
               .Length(2, 200).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.description)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty")
                .Length(2, 1000).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("The field {PropertyName} needs to be higher then {ComparisonValue}");
        }
    }
}
