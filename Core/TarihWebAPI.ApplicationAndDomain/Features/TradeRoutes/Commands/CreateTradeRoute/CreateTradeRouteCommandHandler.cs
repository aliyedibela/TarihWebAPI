using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.CreateTradeRoute
{
    public class CreateTradeRouteCommandHandler : BaseHandler, IRequestHandler<CreateTradeRouteCommandRequest, Guid>
    {
        public CreateTradeRouteCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Guid> Handle(CreateTradeRouteCommandRequest request, CancellationToken cancellationToken)
        {
            var tradeRoute = new TradeRoute
            {
                Name = request.Name,
                AlternativeName = request.AlternativeName,
                StartYear = request.StartYear,
                EndYear = request.EndYear,
                Description = request.Description,
                MainGoods = request.MainGoods,
                StartLocationId = request.StartLocationId,
                EndLocationId = request.EndLocationId,
                RouteGeometryWkt = request.RouteGeometryWkt,
                Importance = request.Importance,
                MapUrl = request.MapUrl,
                WikipediaUrl = request.WikipediaUrl
            };

            await unitOfWork.GetWriteRepository<TradeRoute>().AddAsync(tradeRoute);
            await unitOfWork.SaveAsync();

            return tradeRoute.Id;
        }
    }
}
