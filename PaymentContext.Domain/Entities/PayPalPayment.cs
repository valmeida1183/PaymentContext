namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public string TransactionCode { get; private set; }

    public PayPalPayment(string transactionCode, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string document, string payer, string email, string address) : base(paidDate, expireDate, total, totalPaid, document, payer, email, address)
    {
        TransactionCode = transactionCode;
    }
}