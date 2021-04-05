using FluentValidation;
using TesteCiatecnica.Models.Entities;

namespace TesteCiatecnica.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
			RuleFor(x => x.City)
				.NotNull()
				.WithMessage("O campo Cidade é obrigatório");
			
			RuleFor(x => x.Neighborhood)
				.NotNull()
				.WithMessage("O campo Bairro é obrigatório");

			RuleFor(x => x.Number)
				.NotNull()
				.WithMessage("O campo Número é obrigatório");

			RuleFor(x => x.State)
				.NotNull()
				.WithMessage("O campo UF é obrigatório");

			RuleFor(x => x.State)
				.MaximumLength(2)
				.WithMessage("O campo UF deve ter no máximo 2 caracteres");

			RuleFor(x => x.Street)
				.NotNull()
				.WithMessage("O campo Logradouro é obrigatório");

			RuleFor(x => x.ZipCode)
				.NotNull()
				.WithMessage("O campo CEP é obrigatório");

			RuleFor(x => x.ZipCode)
				.MaximumLength(8)
				.WithMessage("O campo CEP deve ter no máximo 8 caracteres");
		}
    }
}
