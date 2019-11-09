using System;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using Xunit;

namespace PaymentContext.Tests.Handlers
{
    public class SubscriptionHandlerTests
    {
        [Fact]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "gilberto@balta.io2";


            command.BarCode = "123456789123";
            command.BoletoNumber = "987654321";

            command.PaymentNumber = "123123";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);

            command.Total = 100;
            command.TotalPaid = 100;

            command.Payer = "Wayne Corp";

            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "1";

            command.Number = "1";
            command.Neighborhood = "1";
            command.City = "as";
            command.State = "as";
            command.Country = "as";
            command.ZIP = "12345999";


            handler.Handle(command);

            Assert.Equal(false, handler.Valid);

        }
    }
}