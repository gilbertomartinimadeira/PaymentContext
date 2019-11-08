using System;
using System.Linq;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests
{
    public class StudentTests
    {

        private readonly Student _student;
        private readonly Payment _payment;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;

        public StudentTests()
        {
            // arrange
            _name = new Name("Gilberto", "Martini");
            _document = new Document("99999999999", EDocumentType.CPF);
            _email = new Email("gilmartmd@gmail.com");
            _address = new Address("Rua 1","1", "Bairro'", "Cidade", "Estado","pais", "00000-000");

            _student = new Student(_name, _document, _email, _address);                            

            _payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 100, 10, "Bruce", _document,_email,_address,"12345678");
            // Act
            _subscription =new Subscription(DateTime.Now.AddDays(365));

        }
        
        [Fact]
        public void ShouldReturnErrorWhenHasActiveSubscription()
        {
            // act
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            // Assert 
            Assert.True(_student.Invalid);
        }

        [Fact]
        public void ShouldReturnSuccessWhenHasNoActiveSubscription()
        {
            // act
            _subscription.AddPayment(_payment);
            _student.AddSubscription(_subscription);

            // Assert 
            Assert.True(_student.Valid);
        }

        



        [Fact]
        public void ShouldReturnErrorWhenActiveSubscriptionHasNoPayment()
        {
            // arrange
            _student.AddSubscription(_subscription);
            // Act            
            // Assert
            Assert.False(_subscription.Payments.Any());

            Assert.True(_student.Invalid);

            
        }

        

    }
}
