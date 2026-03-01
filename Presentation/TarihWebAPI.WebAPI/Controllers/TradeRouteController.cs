using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.CreateTradeRoute;
using TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.DeleteTradeRoute;
using TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.UpdateTradeRoute;
using TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetAllTradeRoutes;

namespace TarihWebAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeRouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TradeRouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTradeRoutesQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetAllTradeRoutesQueryRequest { Id = id });
            if (response is null) return NotFound(new { message = "Yol rotası bulunamadı." });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTradeRouteCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTradeRouteCommandRequest request)
        {
            request.Id = id;
            var updatedId = await _mediator.Send(request);
            return Ok(new { id = updatedId, message = "Yol rotası başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
                      
            await _mediator.Send(new DeleteTradeRouteCommandRequest { Id = id });
            return Ok(new { message = "Yol rotası başarıyla silindi." });
        }
    }
}
