using CustomerManagement.Application.Contracts.Services;
using CustomerManagement.Application.Helpers;
using FluentEmail.Core;

namespace CustomerManagement.Infrastructure.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public MessageSenderService(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory ?? throw new ArgumentNullException(nameof(fluentEmailFactory));
        }
        public async Task SendEmail(EmailMessageModel emailMessage)
        {
            await _fluentEmailFactory.Create().To(emailMessage.ToAddress)
                .Subject(emailMessage.Subject)
                .Body(emailMessage.Body, true)
                .SendAsync();
        }
    }
}