using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Contracts.Services;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using CustomerManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageSenderService _messageSender;
        private readonly ILogger<SendMessageCommandHandler> _logger;

        public SendMessageCommandHandler(IUnitOfWork unitOfWork, IMessageSenderService messageSender, ILogger<SendMessageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _messageSender = messageSender;
            _logger = logger;
        }

        public async Task<string> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.CommunicationTemplateRepository.Get(request.TemplateId);
            if (template == null)
            {
                throw new NotFoundException($"Template with ID {request.TemplateId} not found.");
            }

            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            var message = template.Body.Replace("{CustomerName}", customer.Name);

            try
            {
                await _messageSender.SendEmail(new EmailMessageModel(customer.Email, template.Subject, message));
            }
            catch
            {
                _logger.LogError("Failed to send email to {Email}", customer.Email);    
            }

            return message;
        }
    }
}