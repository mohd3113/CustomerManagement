using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Commands;
using CustomerManagement.Application.Features.CommunicationTemplates.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CommunicationTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommunicationTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{templateId}")]
        public async Task<ActionResult<TemplateDetailsDto>> GetTemplate(int templateId)
        {
            var query = new GetTemplateQuery { TemplateId = templateId };
            var template = await _mediator.Send(query);
            return Ok(template);
        }

        [HttpGet]
        public async Task<ActionResult<List<TemplateDetailsDto>>> GetTemplates()
        {
            var query = new GetTemplatesQuery();
            var templates = await _mediator.Send(query);
            return Ok(templates);
        }

        [HttpPost]
        public async Task<ActionResult<TemplateDetailsDto>> CreateTemplate([FromBody] CreateUpdateTemplateDto createTemplateDto)
        {
            var command = new CreateTemplateCommand { CreateUpdateTemplateDto = createTemplateDto };
            var template = await _mediator.Send(command);
            return Ok(template);
        }

        [HttpPut("{templateId}")]
        public async Task<ActionResult<TemplateDetailsDto>> UpdateTemplate(int templateId, [FromBody] CreateUpdateTemplateDto updateTemplateDto)
        {
            var command = new UpdateTemplateCommand { TemplateId = templateId, CreateUpdateTemplateDto = updateTemplateDto };
            var template = await _mediator.Send(command);
            return Ok(template);
        }

        [HttpDelete("{templateId}")]
        public async Task<ActionResult<int>> DeleteTemplate(int templateId)
        {
            var command = new DeleteTemplateCommand { TemplateId = templateId };
            var deletedTemplateId = await _mediator.Send(command);
            return Ok(deletedTemplateId);
        }

        [HttpPost("send/{templateId}/{customerId}")]
        public async Task<IActionResult> SendTemplate(int templateId, int customerId)
        {
            var command = new SendMessageCommand { TemplateId = templateId, CustomerId = customerId };
            var message = await _mediator.Send(command);
            return Ok(message);
        }
    }
}