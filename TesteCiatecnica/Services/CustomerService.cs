using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TesteCiatecnica.Data;
using TesteCiatecnica.Models.Entities;
using TesteCiatecnica.Models.Enums;
using TesteCiatecnica.Validators;

namespace TesteCiatecnica.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;
        
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Create(Customer customer)
        {
            if (customer is null)
            {
                throw new ApplicationException("Cliente inválido");
            }

            var validator = new CustomerValidator();
            var results = validator.Validate(customer);

            if (results.Errors.Any())
            {
                var message = "";

                foreach(var error in results.Errors)
                {
                    message += error.ErrorMessage + "|";
                }
                throw new ValidationException(message);
            }

            try
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return "Cliente salvo com sucesso";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return "Cliente não encontrado";
                }
                else
                {
                    return "Erro ao salvar o cliente";
                }
            }

            catch (Exception)
            {
                return "Erro ao salvar o cliente";
            }
        }

        public async Task<ICollection<Customer>> Read()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> Read(int? id)
        {
            if (id is null)
            {
                throw new ApplicationException("Cliente não encontrado");
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer is null)
            {
                throw new ApplicationException("Cliente não encontrado");
            }

            return customer;
        }

        public async Task<string> Update(Customer customer)
        {
            if (customer is null)
            {
                throw new ApplicationException("Cliente inválido");
            }

            if(customer.CustomerType == CustomerTypes.LegalPerson)
            {
                customer.BirthDate = DateTime.MinValue;
            }

            var validator = new CustomerValidator();
            var results = validator.Validate(customer);

            if (results.Errors.Any())
            {
                var message = "";

                foreach (var error in results.Errors)
                {
                    message += error.ErrorMessage + "|";
                }
                throw new ValidationException(message);
            }

            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return "Cliente atualizado com sucesso";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerId))
                {
                    return "Cliente não encontrado";
                }
                else
                {
                    return "Erro ao atualizar os dados do cliente";
                }
            }

            catch (Exception)
            {
                return "Erro ao atualizar os dados do cliente";
            }
        }

        public async Task<string> Delete(int id)
        {
            if (!CustomerExists(id))
            {
                throw new ApplicationException("Cliente não encontrado");
            }

            try
            {
                var customer = await _context.Customers.FindAsync(id);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return "Cliente excluído com sucesso";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return "Cliente não encontrado";
                }
                else
                {
                    return "Erro ao excluir o cliente";
                }
            }

            catch (Exception)
            {
                return "Erro ao excluir o cliente";
            }
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}


