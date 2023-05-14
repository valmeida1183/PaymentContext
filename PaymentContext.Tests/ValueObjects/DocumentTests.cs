using PaymentContext.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    // Red, Green, Refactor
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var document = new Document("123", EDocumentType.CNPJ);
        Assert.IsFalse(document.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var cnpj = "39.811.093/0001-70".Replace(".", String.Empty).Replace("/", String.Empty).Replace("-", String.Empty);
        var document = new Document(cnpj, EDocumentType.CNPJ);
        Assert.IsTrue(document.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInvalid()
    {
        var document = new Document("123", EDocumentType.CPF);
        Assert.IsFalse(document.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("414.876.178-36")]
    [DataRow("374.484.101-48")]
    [DataRow("582.215.955-94")]
    public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
    {
        var clearedCpf = cpf.Replace(".", String.Empty).Replace("-", String.Empty);
        var document = new Document(clearedCpf, EDocumentType.CPF);
        Assert.IsTrue(document.IsValid);
    }
}