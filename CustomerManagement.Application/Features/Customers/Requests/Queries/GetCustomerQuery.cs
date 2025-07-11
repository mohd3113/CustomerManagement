using CustomerManagement.Application.Dtos.Customers;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Queries
{
    public class GetCustomerQuery : IRequest<CustomerDetailsDto>
    {
        public long CustomerId { get; set; }
    }
}