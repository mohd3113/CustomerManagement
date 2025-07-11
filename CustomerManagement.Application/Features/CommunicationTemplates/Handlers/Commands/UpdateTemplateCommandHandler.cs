using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Dtos.CommunicationTemplates.Validation;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Commands
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, TemplateDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTemplateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TemplateDetailsDto> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.CommunicationTemplateRepository.Get(request.TemplateId);

            if (template == null)
            {
                throw new NotFoundException($"Template with ID {request.TemplateId} not found.");
            }

            _mapper.Map(request.CreateUpdateTemplateDto, template);

            var validator = new CreateUpdateTemplateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUpdateTemplateDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation Error", validationResult.Errors.Select(p => p.ErrorMessage).ToList());
            }

            _unitOfWork.CommunicationTemplateRepository.Update(template);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<TemplateDetailsDto>(template);
        }
    }
}