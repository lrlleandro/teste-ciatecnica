using System;
using System.Collections.Generic;
using TesteCiatecnica.Models.Entities;
using TesteCiatecnica.Models.Enums;

namespace TesteCiatecnica.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public CustomerTypes CustomerType { get; set; }

        public string SSNorEIN { get; set; }

        public string NameOrCompanyName { get; set; }

        public string LastNameOrTradingName { get; set; }

        public DateTime BirthDate { get; set; }

        public int AddressId { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }


        public Customer ToCustomer()
        {
            var address = new Address(
                            AddressId, 
                            ZipCode, 
                            Street, 
                            Number, 
                            Complement, 
                            Neighborhood, 
                            City, 
                            State);

            var customer = new Customer(
                            CustomerId,
                            CustomerType,
                            SSNorEIN,
                            NameOrCompanyName,
                            LastNameOrTradingName,
                            BirthDate,
                            address);

            return customer;
        }

        public static List<CustomerViewModel> FromCustomer(ICollection<Customer> customers)
        {
            var list = new List<CustomerViewModel>();

            if (customers is null)
            {
                return list;
            }

            foreach(var customer in customers)
            {
                list.Add(FromCustomer(customer));
            }

            return list;
        }

        public static CustomerViewModel FromCustomer(Customer customer)
        {
            if(customer is null)
            {
                return new CustomerViewModel();

            }

            return new CustomerViewModel
            {
                AddressId = customer.Address.AddressId,
                BirthDate = customer.BirthDate,
                City = customer.Address.City,
                Complement = customer.Address.Complement,
                CustomerId = customer.CustomerId,
                CustomerType = customer.CustomerType,
                LastNameOrTradingName = customer.LastNameOrTradingName,
                NameOrCompanyName = customer.NameOrCompanyName,
                Neighborhood = customer.Address.Neighborhood,
                Number = customer.Address.Number,
                SSNorEIN = customer.SSNorEIN,
                State = customer.Address.State,
                Street = customer.Address.Street,
                ZipCode = customer.Address.ZipCode
            };
        }
    }
}
