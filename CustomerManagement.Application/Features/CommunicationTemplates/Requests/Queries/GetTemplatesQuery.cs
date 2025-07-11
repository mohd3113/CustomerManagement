using CustomerManagement.Application.Dtos.CommunicationTemplates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries
{
    public class GetTemplatesQuery : IRequest<List<TemplateListItemDto>>
    {
    }
}
