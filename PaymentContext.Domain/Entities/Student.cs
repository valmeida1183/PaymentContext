using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions
    {
        get
        {
            return _subscriptions.ToArray();
        }
    }

    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        // Está agrupando as notificações de name, document e email, caso elas existam nas notifications de student
        AddNotifications(name, document, email);
    }

    public void AddSubscription(Subscription subscription)
    {
        // Se já tiver assinatura ativa, cancela
        var hasSubscriptionActive = false;
        foreach (var sub in Subscriptions)
        {
            if (sub.Active)
            {
                hasSubscriptionActive = true;
            }
        }

        AddNotifications(new Contract<Student>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
        );

        // Cancela todas as outras assinaturas e coloca esta como principal
        // foreach (var sub in Subscriptions)
        // {
        //     sub.Inactivate();
        // }

        // _subscriptions.Add(subscription);
    }
}