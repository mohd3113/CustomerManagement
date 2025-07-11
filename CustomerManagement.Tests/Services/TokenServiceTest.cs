using CustomerManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CustomerManagement.Tests.Services
{
    [TestFixture]
    public class TokenServiceTests
    {
        private Mock<IConfiguration> _mockConfig;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private TokenService _tokenService;

        [SetUp]
        public void SetUp()
        {
            _mockConfig = new Mock<IConfiguration>();

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _tokenService = new TokenService(_mockConfig.Object, _mockUserManager.Object);
        }

        [Test]
        public async Task GenerateJwtToken_ReturnsValidToken()
        {
            // Arrange
            var user = new IdentityUser
            {
                Id = "123",
                UserName = "testuser"
            };

            var roles = new List<string> { "Admin", "User" };
            _mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);

            var secretKey = "8743594e795d6a7940bf82902ee7e3902e93003e9e463e6857ea221f9a4d999d"; 
            _mockConfig.Setup(c => c.GetSection("Authentication:Token").Value).Returns(secretKey);

            // Act
            var token = await _tokenService.GenerateJwtToken(user);

            // Assert
            Assert.IsNotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            Assert.IsInstanceOf<JwtSecurityToken>(validatedToken);
        }
    }
}