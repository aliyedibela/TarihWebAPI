using MediatR;
using Microsoft.AspNetCore.Mvc;
using TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.CreateLocation;
using TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.DeleteLocation;
using TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.UpdateLocation;
using TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetAllLocations;
using TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetLocationById;

namespace TarihWebAPI.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllLocationsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetLocationByIdQueryRequest { Id = id });
            if (response is null) return NotFound(new { message = "Lokasyon bulunamadı." });
            return Ok(response);
        }

   
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocationCommandRequest request)
        {
            request.Id = id;
            var updatedId = await _mediator.Send(request);
            return Ok(new { id = updatedId, message = "Lokasyon başarıyla güncellendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteLocationCommandRequest { Id = id });
            return Ok(new { message = "Lokasyon başarıyla silindi." });
        }
    }
}