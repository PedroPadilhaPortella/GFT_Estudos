using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentsTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentsTests()
        {
            _name = new Name("Pedro", "Portella");
            _document = new Document("17535258964", EDocumentType.CPF);
            _email = new Email("pedro@gmail.com");
            _address = new Address("Rua 1", "12", "Jd Angela", "São Paulo", "SP", "Brasil", "556745432");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }



        [TestMethod]
        public void ShoudReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(5), 10, 10, "Portella Market", _document, _address, _email);
            
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        public void ShoudReturnErrorWhenScriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }


        [TestMethod]
        public void ShoudReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Portella Market", _document, _address, _email);
           _subscription.AddPayment(payment);
           _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
    }
}