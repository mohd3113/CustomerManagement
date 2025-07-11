using CustomerManagement.Application.Contracts.Services;
using CustomerManagement.Application.Dtos.Auth;
using CustomerManagement.Application.Features.Auth.Requests.Command;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Application.Features.Auth.Handlers.Command
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.SignInDto.Username);
            if (user == null)
            {
                return new SignInResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.SignInDto.Password);
            if (!result)
            {
                return new SignInResponse
                {
                    IsSuccess = false,
                    Message = "Invalid password"
                };
            }

            var token = await _tokenService.GenerateJwtToken(user);
            return new SignInResponse
            {
                IsSuccess = true,
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Message = "Sign in successful"
            };
        }
    }
}
