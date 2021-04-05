using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteCiatecnica.Models.Entities
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(int addressId, string zipCode, string street, string number, string complement, string neighborhood, string city, string state)
        {
            AddressId = addressId;
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        public int AddressId { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
