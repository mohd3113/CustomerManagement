using CustomerManagement.Application.Dtos.CommunicationTemplates;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries
{
    public class GetTemplateQuery : IRequest<TemplateDetailsDto>
    {
        public int TemplateId { get; set; }
    }
}
