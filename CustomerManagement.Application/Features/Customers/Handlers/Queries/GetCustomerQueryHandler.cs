using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.Customers.Requests.Queries;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDetailsDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.Get(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {request.CustomerId} not found.");
            }

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}