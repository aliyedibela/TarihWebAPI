using MediatR;
using Microsoft.AspNetCore.Mvc;
using TarihWebAPI.ApplicationAndDomain.Features.States.Commands.CreateState;
using TarihWebAPI.ApplicationAndDomain.Features.States.Commands.DeleteState;
using TarihWebAPI.ApplicationAndDomain.Features.States.Commands.UpdateState;
using TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetAllStates;
using TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetStateById;

namespace TarihWebAPI.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStatesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetStateByIdQueryRequest { Id = id });
            if (response is null) return NotFound(new { message = "Devlet bulunamadı." });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStateCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStateCommandRequest request)
        {
            request.Id = id;
            var updatedId = await _mediator.Send(request);
            return Ok(new { id = updatedId, message = "Devlet başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteStateCommandRequest { Id = id });
            return Ok(new { message = "Devlet başarıyla silindi." });
        }
    }
}