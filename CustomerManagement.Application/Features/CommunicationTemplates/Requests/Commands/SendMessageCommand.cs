using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands
{
    public class SendMessageCommand : IRequest<string>
    {
        public int TemplateId { get; set; }

        public long CustomerId { get; set; }
    }
}