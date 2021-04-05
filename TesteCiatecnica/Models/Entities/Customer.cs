using System;
using TesteCiatecnica.Models.Enums;

namespace TesteCiatecnica.Models.Entities
{
    public class Customer
    {
        public Customer() { }

        public Customer(
                int customerId, 
                CustomerTypes customerType, 
                string sSNorEIN, 
                string nameOrCompanyName, 
                string lastNameOrTradingName, 
                DateTime birthDate, 
                Address address)
        {
            CustomerId = customerId;
            CustomerType = customerType;
            SSNorEIN = sSNorEIN;
            NameOrCompanyName = nameOrCompanyName;
            LastNameOrTradingName = lastNameOrTradingName;
            BirthDate = birthDate;
            Address = address;
        }

        public int CustomerId { get; set; }

        public CustomerTypes CustomerType { get; set; }

        public string SSNorEIN { get; set; }

        public string NameOrCompanyName { get; set; }

        public string LastNameOrTradingName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual Address Address { get; set; }
    }
}