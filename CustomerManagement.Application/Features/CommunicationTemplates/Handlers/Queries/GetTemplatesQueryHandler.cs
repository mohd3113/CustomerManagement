using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Queries
{
    public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, List<TemplateListItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTemplatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<TemplateListItemDto>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
            var templates = await _unitOfWork.CommunicationTemplateRepository.GetAll();
            return _mapper.Map<List<TemplateListItemDto>>(templates);
        }
    }
}