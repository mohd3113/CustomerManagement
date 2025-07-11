using MediatR;

namespace CustomerManagement.Application.Features.Customers.Requests.Commands
{
    public class DeleteCsutomerCommand : IRequest<long>
    {
        public long CustomerId { get; set; }
    }
}