using CustomerManagement.Application.Dtos.Customers;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Commands
{
    public class AddCustomerCommand : IRequest<CustomerDetailsDto>
    {
        public CreateUpdateCustomerDto Customer { get; set; }
    }
}