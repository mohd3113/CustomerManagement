using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Features.Customers.Requests.Commands;
using CustomerManagement.Domain;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Commands
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDetailsDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);

            await _unitOfWork.CustomerRepository.Add(customer);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}