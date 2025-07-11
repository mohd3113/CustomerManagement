using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.Customers.Requests.Commands;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Commands
{
    public class DeleteCsutomerCommandHandler : IRequestHandler<DeleteCsutomerCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCsutomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<long> Handle(DeleteCsutomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            _unitOfWork.CustomerRepository.HardDelete(customer);

            await _unitOfWork.SaveChanges();

            return request.CustomerId;
        }
    }
}