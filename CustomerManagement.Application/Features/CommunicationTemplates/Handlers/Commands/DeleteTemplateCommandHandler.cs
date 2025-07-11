using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using MediatR;

namespace CustomerManagement.Application.Features.CommunicationTemplates.Handlers.Commands
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTemplateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.CommunicationTemplateRepository.Get(request.TemplateId);

            if (template == null)
            {
                throw new NotFoundException($"Template with ID {request.TemplateId} not found.");
            }

            _unitOfWork.CommunicationTemplateRepository.HardDelete(template);

            await _unitOfWork.SaveChanges();

            return request.TemplateId;
        }
    }
}