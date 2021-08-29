using FluentValidation;
using FullCatalog.Business.Models.Validations.Documents;

namespace FullCatalog.Business.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty")
                .Length(2, 200).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            When(s => s.SupplierType == SupplierType.NaturalPerson, () =>
            {
                RuleFor(s => s.DocumentNumber.Length).Equal(CpfValidacao.CpfLength)
                    .WithMessage("The field Document Number must have between {ComparasionValue}, but received {PropertyValue}.");
                RuleFor(s => CpfValidacao.Validate(s.DocumentNumber)).Equal(true)
                    .WithMessage("The document number is not valid!");

            });

            When(s => s.SupplierType == SupplierType.LegalPerson, () =>
            {
                RuleFor(s => s.DocumentNumber.Length).Equal(CnpjValidation.CnpjLength)
                    .WithMessage("The field Document Number must have between {ComparasionValue}, but received {PropertyValue}.");
                RuleFor(s => CnpjValidation.Validate(s.DocumentNumber)).Equal(true)
                    .WithMessage("The document number is not valid!");

            });
        }
    }
}
