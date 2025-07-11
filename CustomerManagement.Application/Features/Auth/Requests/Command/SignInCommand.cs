using CustomerManagement.Application.Dtos.Auth;
using MediatR;

namespace CustomerManagement.Application.Features.Auth.Requests.Command
{
    public class SignInCommand : IRequest<SignInResponse>
    {
        public SignInDto SignInDto { get; set; }
    }
}
