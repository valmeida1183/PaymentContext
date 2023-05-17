using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IEmailService _emailService;
    private readonly IStudentRepository _studentRepository;

    public SubscriptionHandler(IEmailService emailService, IStudentRepository studentRepository)
    {
        _emailService = emailService;
        _studentRepository = studentRepository;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        //Fail fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não possível realizar seu cadastro");
        }

        // Verificar se Documento já está cadastrado
        if (_studentRepository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Este CPF já está em uso");
        }

        // verificar se email já está cadastrado
        if (_studentRepository.EmailExists(command.Email))
        {
            AddNotification("Email", "Este email já está em uso");
        }

        // Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

        // Gerar as Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            payerDocument,
            command.Payer,
            email,
            address
        );

        //Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Salvar as informações
        _studentRepository.CreateSubscription(student);

        // Enviar email de boas vindas
        _emailService.Send(student.Name.FullName(), student.Email.Address, "Bem vindo ao Balta.io", "Sua assinatura foi criada!");

        // Retornar informações

        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}