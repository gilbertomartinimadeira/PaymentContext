using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

using Xunit;

namespace PaymentContext.Tests.ValueObjects
{
    public class DocumentTests
    {

        // Red, Green, Refactor
        [Fact]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            // Arrange, Act, Assert
            var sut = new Document("123", EDocumentType.CNPJ);
            
            Assert.True(sut.Invalid);
        }

        [Fact]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            // Arrange, Act, Assert
            var sut = new Document("75283978000107", EDocumentType.CNPJ);
            
            Assert.True(sut.Valid);
        }
        [Fact]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var sut = new Document("123", EDocumentType.CPF);

            Assert.True(sut.Invalid);

        }
    
        [Theory]
        [InlineData("11102768774")]
        [InlineData("10621058777")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            var sut = new Document(cpf, EDocumentType.CPF);

            Assert.True(sut.Valid);
        }
    }
}