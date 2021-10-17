using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    public class CreateBoletoSubscriptionCommandTest
    {
        // Red, Green, Refactor

        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }
    }
}