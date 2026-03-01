using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetTradeRouteById
{
    public class GetTradeRouteByIdQueryHandler : BaseHandler, IRequestHandler<GetTradeRouteByIdQueryRequest, GetTradeRouteByIdQueryResponse>
    {
        public GetTradeRouteByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public Task<GetTradeRouteByIdQueryResponse> Handle(GetTradeRouteByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var tradeRoute = unitOfWork.GetReadRepository<TradeRoute>()
                .GetAsync(
                predicate: tr => tr.Id == request.Id && tr.IsActive,
                include: 
                x => x.Include(x => x.StartLocation).
                Include(x => x.EndLocation)).Result;


            if (tradeRoute == null)
            {
                throw new Exception("Trade route not found");
            }

            var response = new GetTradeRouteByIdQueryResponse
            {
                Id = tradeRoute.Id,
                Name = tradeRoute.Name,
                AlternativeName = tradeRoute.AlternativeName,
                StartYear = tradeRoute.StartYear,
                EndYear = tradeRoute.EndYear,
                Description = tradeRoute.Description,
                MainGoods = tradeRoute.MainGoods,
                StartLocationId = tradeRoute.StartLocationId,
                EndLocationId = tradeRoute.EndLocationId,
                RouteGeometryWkt = tradeRoute.RouteGeometryWkt,
                Importance = tradeRoute.Importance,
                MapUrl = tradeRoute.MapUrl,
                WikipediaUrl = tradeRoute.WikipediaUrl,
                StartLocation = tradeRoute.StartLocation,
                EndLocation = tradeRoute.EndLocation
            };
            return Task.FromResult(response);
        }
    }
}
