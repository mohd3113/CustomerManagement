using CustomerManagement.Application.Dtos.Customers;
using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Queries
{
    public class GetCustomersQuery : IRequest<List<CustomerDetailsDto>>
    {
        
    }
}