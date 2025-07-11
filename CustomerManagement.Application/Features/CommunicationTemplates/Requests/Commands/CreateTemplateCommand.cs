using CustomerManagement.Application.Dtos.CommunicationTemplates;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands
{
    public class CreateTemplateCommand : IRequest<TemplateDetailsDto>
    {
        public CreateUpdateTemplateDto CreateUpdateTemplateDto { get; set; }
    }
}