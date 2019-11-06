using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zIP)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZIP = zIP;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Street, 3,"Address.Street", "Street name is too short, use at least 3 characters")
            );
        }

        public string Street { get; private set; }

        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZIP { get; private set; }
    }
}