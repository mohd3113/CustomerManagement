using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Features.Customers.Requests.Commands;
using CustomerManagement.Domain;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomerDetailsDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), request.CustomerId);
            }

            _mapper.Map(request.Customer, customer);

            _unitOfWork.CustomerRepository.Update(customer);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}
