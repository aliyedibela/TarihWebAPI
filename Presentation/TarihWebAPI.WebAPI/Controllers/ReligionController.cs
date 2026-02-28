using MediatR;
using Microsoft.AspNetCore.Mvc;
using TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.CreateReligion;
using TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.DeleteReligion;
using TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.UpdateReligion;
using TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetAllReligions;
using TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetReligionById;

namespace TarihWebAPI.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReligionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllReligionsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetReligionByIdQueryRequest { Id = id });
            if (response is null) return NotFound(new { message = "Din bulunamadı." });
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReligionCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReligionCommandRequest request)
        {
            request.Id = id;
            var updatedId = await _mediator.Send(request);
            return Ok(new { id = updatedId, message = "Din başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteReligionCommandRequest { Id = id });
            return Ok(new { message = "Din başarıyla silindi." });
        }
    }
}