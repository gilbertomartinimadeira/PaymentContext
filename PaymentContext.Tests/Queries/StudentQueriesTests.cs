using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests.Queries
{
    public class StudentQueriesTests
    {

        
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();

            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i, EDocumentType.CPF),
                    new Email(i+"@balta.io2"),
                    new Address("1","","","","","","")
                    ));    
            }
        }

        [Fact]
        public void ShouldReturnNullWhenDocumentIsMissing()
        {
            var exp = StudentQueries.GetStudentInfo("99999999999");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.Null(student);

        }

        [Fact]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");

            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.NotNull(student);

        }
    }
}