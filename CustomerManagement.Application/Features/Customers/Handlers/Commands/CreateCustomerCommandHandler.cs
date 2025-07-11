using AutoMapper;
using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Dtos.Customers.Validation;
using CustomerManagement.Application.Exceptions;
using CustomerManagement.Application.Features.Customers.Requests.Commands;
using CustomerManagement.Domain;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDetailsDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateUpdateCustomerDtoValidator();

            var validationResult = await validator.ValidateAsync(request.Customer, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation Error", validationResult.Errors.Select(p => p.ErrorMessage).ToList());
            }

            var customer = _mapper.Map<Customer>(request.Customer);

            await _unitOfWork.CustomerRepository.Add(customer);

            await _unitOfWork.SaveChanges();

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}