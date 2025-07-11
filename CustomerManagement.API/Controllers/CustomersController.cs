using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Application.Features.Customers.Requests.Commands;
using CustomerManagement.Application.Features.Customers.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDetailsDto>> GetCustomer(long customerId)
        {
            var query = new GetCustomerQuery { CustomerId = customerId };
            var customer = await _mediator.Send(query);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDetailsDto>>> GetCustomers()
        {
            var query = new GetCustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDetailsDto>> CreateCustomer([FromBody] CreateUpdateCustomerDto createCustomerDto)
        {
            var command = new CreateCustomerCommand { Customer = createCustomerDto };
            var customer = await _mediator.Send(command);
            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult<CustomerDetailsDto>> UpdateCustomer(long customerId, [FromBody] CreateUpdateCustomerDto updateCustomerDto)
        {
            var command = new UpdateCustomerCommand { CustomerId = customerId, Customer = updateCustomerDto };
            var customer = await _mediator.Send(command);
            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<long>> DeleteCustomer(long customerId)
        {
            var command = new DeleteCsutomerCommand { CustomerId = customerId };
            var deletedCustomerId = await _mediator.Send(command);
            return Ok(deletedCustomerId);
        }
    }
}