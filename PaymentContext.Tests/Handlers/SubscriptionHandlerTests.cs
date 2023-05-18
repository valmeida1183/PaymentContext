using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handler;

[TestClass]
public class SubscriptionHandlerTests
{
    // Red, Green, Refactor
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeEmailService(), new FakeStudentRepository());
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Bruce";
        command.LastName = "Wayne";
        command.Document = "999999999";
        command.Email = "hello@teste2.com";
        command.BarCode = "123456789";
        command.BoletoNumber = "12345678";
        command.PaymentNumber = "123121";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "WAYNE CORP";
        command.PayerDocument = "12345678911";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "batman@dc.com";
        command.Street = "Rua A";
        command.Number = "12";
        command.Neighborhood = "Bairro B";
        command.City = "Gotham";
        command.State = "SP";
        command.Country = "BR";
        command.ZipCode = "12345678";

        handler.Handle(command);
        Assert.IsFalse(handler.IsValid);
    }
}