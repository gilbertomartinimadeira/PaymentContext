using PaymentContext.Domain.Commands;
using Xunit;

namespace PaymentContext.Tests.Commands
{
    public class CreateBoletoSubscriptionCommandTests
    {
        [Fact]
        public void ShouldreturnErrorWhenNameIsInvalid()
        {

            var command = new CreateBoletoSubscriptionCommand();    

            command.FirstName = "";

            command.Validate();

            Assert.Equal(false,command.Valid);

            
        }

    }
}