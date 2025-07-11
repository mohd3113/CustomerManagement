using CustomerManagement.API.Controllers;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustomerManagement.Tests.Controllers
{
    [TestFixture]
    public class CommunicationTemplatesControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private CommunicationTemplatesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CommunicationTemplatesController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetTemplate_ReturnsTemplateDetailsDto()
        {
            var templateId = 1;
            var expectedTemplate = new TemplateDetailsDto
            {
                CommunicationTemplateId = templateId,
                Name = "Test",
                Subject = "Subject",
                Body = "Body"
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetTemplateQuery>(q => q.TemplateId == templateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTemplate);

            var result = await _controller.GetTemplate(templateId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedTemplate, okResult.Value);
        }

        [Test]
        public async Task GetTemplates_ReturnsListOfTemplateDetailsDto()
        {
            var expectedTemplates = new List<TemplateListItemDto>
            {
                new TemplateListItemDto { CommunicationTemplateId = 1, Name = "A", Subject = "S1"},
                new TemplateListItemDto { CommunicationTemplateId = 2, Name = "B", Subject = "S2"}
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetTemplatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTemplates);

            var result = await _controller.GetTemplates();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedTemplates, okResult.Value);
        }

        [Test]
        public async Task CreateTemplate_ReturnsCreatedTemplateDetailsDto()
        {
            var createDto = new CreateUpdateTemplateDto
            {
                Name = "New",
                Subject = "Subj",
                Body = "Body"
            };
            var expectedTemplate = new TemplateDetailsDto
            {
                CommunicationTemplateId = 3,
                Name = createDto.Name,
                Subject = createDto.Subject,
                Body = createDto.Body
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<CreateTemplateCommand>(c => c.CreateUpdateTemplateDto == createDto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTemplate);

            var result = await _controller.CreateTemplate(createDto);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedTemplate, okResult.Value);
        }

        [Test]
        public async Task UpdateTemplate_ReturnsUpdatedTemplateDetailsDto()
        {
            var templateId = 2;
            var updateDto = new CreateUpdateTemplateDto
            {
                Name = "Updated",
                Subject = "UpdatedSubj",
                Body = "UpdatedBody"
            };
            var expectedTemplate = new TemplateDetailsDto
            {
                CommunicationTemplateId = templateId,
                Name = updateDto.Name,
                Subject = updateDto.Subject,
                Body = updateDto.Body
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<UpdateTemplateCommand>(c => c.TemplateId == templateId && c.CreateUpdateTemplateDto == updateDto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTemplate);

            var result = await _controller.UpdateTemplate(templateId, updateDto);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedTemplate, okResult.Value);
        }

        [Test]
        public async Task DeleteTemplate_ReturnsDeletedTemplateId()
        {
            var templateId = 5;
            _mediatorMock
                .Setup(m => m.Send(It.Is<DeleteTemplateCommand>(c => c.TemplateId == templateId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(templateId);

            var result = await _controller.DeleteTemplate(templateId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(templateId, okResult.Value);
        }

        [Test]
        public async Task SendTemplate_ReturnsOkWithMessage()
        {
            var templateId = 7;
            var customerId = 99;
            var expectedMessage = "Message sent";

            _mediatorMock
                .Setup(m => m.Send(It.Is<SendMessageCommand>(c => c.TemplateId == templateId && c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedMessage);

            var result = await _controller.SendTemplate(templateId, customerId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedMessage, okResult.Value);
        }
    }
}