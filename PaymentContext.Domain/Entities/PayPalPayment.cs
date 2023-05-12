using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public string TransactionCode { get; private set; }

    public PayPalPayment(
        string transactionCode,
        DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        Document document,
        string payer,
        Email email,
        Address address) : base(paidDate, expireDate, total, totalPaid, document, payer, email, address)
    {
        TransactionCode = transactionCode;
    }
}