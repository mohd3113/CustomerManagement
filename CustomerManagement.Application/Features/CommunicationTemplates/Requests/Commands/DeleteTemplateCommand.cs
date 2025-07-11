using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands
{
    public class DeleteTemplateCommand : IRequest<int>
    {
        public int TemplateId { get; set; }
    }
}
