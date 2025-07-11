using CustomerManagement.Application.Dtos.CommunicationTemplates;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries
{
    public class GetTemplatesQuery : IRequest<List<TemplateListItemDto>>
    {
    }
}
