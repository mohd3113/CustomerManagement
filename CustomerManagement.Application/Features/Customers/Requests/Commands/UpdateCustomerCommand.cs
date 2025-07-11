using CustomerManagement.Application.Dtos.Customers;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Commands
{
    public class UpdateCustomerCommand : IRequest<CustomerDetailsDto>
    {
        public long CustomerId { get; set; }

        public CreateUpdateCustomerDto Customer { get; set; }
    }
}