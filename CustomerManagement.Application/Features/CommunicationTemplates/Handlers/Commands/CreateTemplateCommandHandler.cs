using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Dtos.CommunicationTemplates.Validation;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using CustomerManagement.Domain;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Commands
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, TemplateDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTemplateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TemplateDetailsDto> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = _mapper.Map<CommunicationTemplate>(request.CreateUpdateTemplateDto);

            var validator = new CreateUpdateTemplateDtoValidator();

            var validationResult = validator.Validate(request.CreateUpdateTemplateDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation Error", validationResult.Errors.Select(p => p.ErrorMessage).ToList());
            }

            await _unitOfWork.CommunicationTemplateRepository.Add(template);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<TemplateDetailsDto>(template);
        }
    }
}