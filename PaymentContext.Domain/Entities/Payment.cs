using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public abstract class Payment : Entity
{
    public string Number { get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public decimal Total { get; private set; }
    public decimal TotalPaid { get; private set; }
    public Document Document { get; private set; }
    public string Payer { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }

    protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Document document, string payer, Email email, Address address)
    {
        Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper(); // criar extension method para isso.
        PaidDate = paidDate;
        ExpireDate = expireDate;
        Total = total;
        TotalPaid = totalPaid;
        Document = document;
        Payer = payer;
        Email = email;
        Address = address;

        AddNotifications(new Contract<Payment>()
            .Requires()
            .IsGreaterThan(Total, 0, "Payment.Total", "O total não pode ser zero")
            .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor do pagamento")
        );
    }

}