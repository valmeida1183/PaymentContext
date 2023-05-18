using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks;

public class FakeStudentRepository : IStudentRepository
{
    public void CreateSubscription(Student student)
    {

    }

    public bool DocumentExists(string document)
    {
        return document == "999999999";
    }

    public bool EmailExists(string email)
    {
        return email == "hello@teste.com";
    }
}