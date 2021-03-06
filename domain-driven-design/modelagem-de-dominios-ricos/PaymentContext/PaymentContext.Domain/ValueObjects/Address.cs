using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : Entity
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
        {
            this.Street = street;
            this.Number = number;
            this.Neighborhood = neighborhood;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipCode;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Street, 3, "Address.Street", "A rua deve contem minimo de 3 caracteres")
            );
        }
    }
}