using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Features.Customers.Requests.Queries;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDetailsDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAll();

            return _mapper.Map<List<CustomerDetailsDto>>(customers);
        }
    }
}