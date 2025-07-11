using CustomerManagement.Application.Dtos.Auth;
using CustomerManagement.Application.Features.Auth.Requests.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            var command = new SignInCommand { SignInDto = signInDto };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}