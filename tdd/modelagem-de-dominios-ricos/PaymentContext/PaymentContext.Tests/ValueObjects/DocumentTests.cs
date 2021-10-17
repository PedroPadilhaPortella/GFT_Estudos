using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor

        [TestMethod]
        [DataTestMethod]
        [DataRow("32")]
        [DataRow("1")]
        [DataRow("3123132133333123123123554786876838637")]
        [DataRow("35643")]
        public void ShouldReturnErrorWhenCNPJIsInvalid(string cnpj)
        {
            var document = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("49023448000110")]
        [DataRow("29285811000168")]
        [DataRow("13857639000130")]
        [DataRow("44844816000177")]
        public void ShouldReturnSuccessWhenCNPJIsValid(string cnpj)
        {
            var document = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(document.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("4")]
        [DataRow("34")]
        [DataRow("6575675675676575675765765765756765")]
        [DataRow("77")]
        public void ShouldReturnErrorWhenCPFIsInvalid(string cpf)
        {
            var document = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("23392118400")]
        [DataRow("53747823475")]
        [DataRow("36588508255")]
        [DataRow("94426595290")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            var document = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(document.Valid);
        }  
    }
}