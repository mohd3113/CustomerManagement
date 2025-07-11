using CustomerManagement.Application.Helpers;
using CustomerManagement.Infrastructure.Services;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Moq;

namespace CustomerManagement.Tests.Services
{
    [TestFixture]
    public class MessageSenderServiceTest
    {
        private Mock<IFluentEmailFactory> _fluentEmailFactoryMock;
        private Mock<IFluentEmail> _fluentEmailMock;
        private MessageSenderService _service;

        [SetUp]
        public void SetUp()
        {
            _fluentEmailFactoryMock = new Mock<IFluentEmailFactory>();
            _fluentEmailMock = new Mock<IFluentEmail>();

            // Setup fluent email chain
            _fluentEmailFactoryMock.Setup(f => f.Create()).Returns(_fluentEmailMock.Object);
            _fluentEmailMock.Setup(e => e.To(It.IsAny<string>())).Returns(_fluentEmailMock.Object);
            _fluentEmailMock.Setup(e => e.Subject(It.IsAny<string>())).Returns(_fluentEmailMock.Object);
            _fluentEmailMock.Setup(e => e.Body(It.IsAny<string>(), true)).Returns(_fluentEmailMock.Object);
            _fluentEmailMock.Setup(e => e.SendAsync(null)).ReturnsAsync(new SendResponse());

            _service = new MessageSenderService(_fluentEmailFactoryMock.Object);
        }

        [Test]
        public void Constructor_NullFactory_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MessageSenderService(null));
        }

        [Test]
        public async Task SendEmail_ValidModel_CallsFluentEmailFactoryAndSendsEmail()
        {
            var emailModel = new EmailMessageModel("test@example.com", "Subject", "Body");

            await _service.SendEmail(emailModel);

            _fluentEmailFactoryMock.Verify(f => f.Create(), Times.Once);
            _fluentEmailMock.Verify(e => e.To(emailModel.ToAddress), Times.Once);
            _fluentEmailMock.Verify(e => e.Subject(emailModel.Subject), Times.Once);
            _fluentEmailMock.Verify(e => e.Body(emailModel.Body, true), Times.Once);
            _fluentEmailMock.Verify(e => e.SendAsync(null), Times.Once);
        }
    }
}
