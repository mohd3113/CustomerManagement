using CustomerManagement.Application.Dtos.CommunicationTemplates;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands
{
    public class UpdateTemplateCommand : IRequest<TemplateDetailsDto>
    {
        public int TemplateId { get; set; }

        public CreateUpdateTemplateDto CreateUpdateTemplateDto { get; set; }
    }
}