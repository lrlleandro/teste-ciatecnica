using Castle.Core.Internal;
using FluentValidation;
using System;
using TesteCiatecnica.Models.Entities;
using TesteCiatecnica.Models.Enums;

namespace TesteCiatecnica.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Address).SetValidator(new AddressValidator());

            RuleFor(x => x.BirthDate)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo Data de Nascimento é obrigatório");

            RuleFor(x => x.BirthDate)
                .Empty()
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("O campo Data de Nascimento não deve ser preenchido para Pessoa Jurídica");

            RuleFor(x => x.BirthDate)
                .Must(BeOver19)
                .WithMessage("A idade mínima para cadastro é de 19 anos");

            RuleFor(x => x.CustomerType)
                .NotNull()
                .WithMessage("O campo é obrigatório");

            RuleFor(x => x.LastNameOrTradingName)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("O campo Nome Fantasia é obrigatório");

            RuleFor(x => x.LastNameOrTradingName)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo Sobrenome é obrigatório");

            RuleFor(x => x.LastNameOrTradingName)
                .MaximumLength(15)
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo Sobrenome deve ter no máximo 15 caracteres");

            RuleFor(x => x.NameOrCompanyName)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo Nome é obrigatório");

            RuleFor(x => x.NameOrCompanyName)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("O campo Razão Social é obrigatório");

            RuleFor(x => x.SSNorEIN)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo CPF é obrigatório");

            RuleFor(x => x.SSNorEIN)
                .NotNull()
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("O campo CNPJ é obrigatório");

            RuleFor(x => x.SSNorEIN)
                .MaximumLength(11)
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("O campo CPF deve ter no máximo 11 caracteres");


            RuleFor(x => x.SSNorEIN).MaximumLength(14)
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("O campo CNPJ deve ter no máximo 14 caracteres");

            RuleFor(x => x.SSNorEIN)
                .Must(IsCpf)
                .When(Customer => Customer.CustomerType == CustomerTypes.PhysicalPerson)
                .WithMessage("Campo CPF inválido");

            RuleFor(x => x.SSNorEIN)
                .Must(IsCnpj)
                .When(Customer => Customer.CustomerType == CustomerTypes.LegalPerson)
                .WithMessage("Campo CNPJ inválido");
        }

        protected bool BeOver19(DateTime date)
        {
            var result = (date.AddYears(19) <= DateTime.Now);
            return result;
        }

        protected bool IsCnpj(string cnpj)
        {
            if (cnpj.IsNullOrEmpty())
            {
                return false;
            }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        protected bool IsCpf(string cpf)
        {
            if (cpf.IsNullOrEmpty())
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}