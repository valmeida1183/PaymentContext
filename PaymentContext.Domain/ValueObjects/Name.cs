using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
            .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
            .IsLowerOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres")
            .IsLowerOrEqualsThan(LastName, 40, "Name.LastName", "Sobrenome deve conter até 40 caracteres")
        );
    }

    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }
}
