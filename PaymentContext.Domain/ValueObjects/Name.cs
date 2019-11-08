using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

           AddNotifications(new Contract()
           .Requires()
           .HasMinLen(FirstName, 3,"Name.FirstName","FirstName too short, use at least 3 characters")
           .HasMinLen(LastName, 3,"Name.LastName","LastName too short, use at least 3 characters")
           .HasMaxLen(LastName,40, "Name.LastName", "Lastname too long, exceeded 40 characteres")
           );
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public  override string ToString() => $"{FirstName} {LastName}";
    }
}