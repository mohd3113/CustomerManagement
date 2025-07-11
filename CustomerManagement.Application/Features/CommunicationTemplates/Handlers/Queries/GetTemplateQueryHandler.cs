using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Queries
{
    public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, TemplateDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTemplateQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TemplateDetailsDto> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.CommunicationTemplateRepository.Get(request.TemplateId);

            if (template == null)
            {
                throw new NotFoundException($"Template with ID {request.TemplateId} not found.");
            }

            return _mapper.Map<TemplateDetailsDto>(template);
        }
    }
}
