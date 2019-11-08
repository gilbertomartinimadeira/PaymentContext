using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
             _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();

            if (!command.Valid)
            {
                AddNotifications(command);
                new CommandResult(false, "Não foi possível realizar o cadastro");
            }

            // Verificar se documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está em uso");
            }

            // Verificar se email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification("Document", "Este Email já está em uso");
            }

            // Gerar os VOs 
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZIP);

            // Gerar as entidades
            var student = new Student(name, document,email, address);
            var subscription = new Subscription(null);
            var boletoPayment = new BoletoPayment(
                DateTime.Now, 
                DateTime.Now.AddMonths(1),
                command.Total,
                command.TotalPaid,            
                command.Payer,
                new Document(command.PayerDocument,command.PayerDocumentType),        
                email,
                address,
                command.BarCode,
                command.BoletoNumber
                );


            // relacionamentos
            subscription.AddPayment(boletoPayment);
            student.AddSubscription(subscription);

            // Agrupar as validaçoes
            AddNotifications(name,document,email,address, student, subscription, boletoPayment);

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), command.Email,"Bem vindo ao balta.io","Sua assinatura foi criada");
            
            // retornar informações
            return new CommandResult(true, "assinatura realizada com sucesso");
        }
    }
}