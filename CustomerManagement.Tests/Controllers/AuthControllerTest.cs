using CustomerManagement.API.Controllers;
using CustomerManagement.Application.Dtos.Auth;
using CustomerManagement.Application.Features.Auth.Requests.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustomerManagement.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private AuthController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Test]
        public async Task SignIn_ReturnsOk_WhenResponseIsSuccess()
        {
            // Arrange
            var signInDto = new SignInDto { Username = "user", Password = "pass" };
            var command = new SignInCommand { SignInDto = signInDto };
            var response = new SignInResponse { IsSuccess = true, Message = "Success" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.SignIn(signInDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public async Task SignIn_ReturnsBadRequest_WhenResponseIsNotSuccess()
        {
            // Arrange
            var signInDto = new SignInDto { Username = "user", Password = "wrongpass" };
            var command = new SignInCommand { SignInDto = signInDto };
            var response = new SignInResponse { IsSuccess = false, Message = "Invalid credentials" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<SignInCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.SignIn(signInDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(response.Message, badRequestResult.Value);
        }
    }
}