using PaymentContext.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("41487617836", EDocumentType.CPF);
        _email = new Email("batman@dc.com");
        _address = new Address("Rua 1", "12", "Bairro bom", "Gotham", "SP", "BR", "13400000");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _document, "WAYNE CORP", _email, _address);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _document, "WAYNE CORP", _email, _address);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.IsValid);
    }
}