using CustomerManagement.Application.Dtos.Customers;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDetailsDto>
    {
        public CreateUpdateCustomerDto Customer { get; set; }
    }
}