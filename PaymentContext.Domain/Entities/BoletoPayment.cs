using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class BoletoPayment : Payment
{
    public string BarCode { get; private set; }
    public string BoletoNumber { get; private set; }

    public BoletoPayment(
        string barCode,
        string boletoNumber,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        Document document,
        string payer,
        Email email,
        Address address) : base(paidDate, expireDate, total, totalPaid, document, payer, email, address)
    {
        BarCode = barCode;
        BoletoNumber = boletoNumber;
    }

}