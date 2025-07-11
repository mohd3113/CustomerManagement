using Microsoft.AspNetCore.Identity;

namespace CustomerManagement.Application.Contracts.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}