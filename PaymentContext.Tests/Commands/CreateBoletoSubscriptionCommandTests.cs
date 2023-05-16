using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    // Red, Green, Refactor
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = String.Empty;

        command.Validate();
        Assert.IsFalse(command.IsValid);
    }
}