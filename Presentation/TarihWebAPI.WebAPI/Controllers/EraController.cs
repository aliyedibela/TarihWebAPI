using MediatR;
using Microsoft.AspNetCore.Mvc;
using TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.CreateEra;
using TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.DeleteEra;
using TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.UpdateEra;
using TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetAllEras;
using TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetEraById;

namespace TarihWebAPI.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllErasQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetEraByIdQueryRequest { Id = id });
            if (response is null) return NotFound(new { message = "Çağ bulunamadı." });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEraCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEraCommandRequest request)
        {
            request.Id = id;
            var updatedId = await _mediator.Send(request);
            return Ok(new { id = updatedId, message = "Çağ başarıyla güncellendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteEraCommandRequest { Id = id });
            return Ok(new { message = "Çağ başarıyla silindi." });
        }
    }
}