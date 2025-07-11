using CustomerManagement.Application.Helpers;

namespace CustomerManagement.Application.Contracts.Services
{
    public interface IMessageSenderService
    {
        Task SendEmail(EmailMessageModel emailMessage);
    }
}