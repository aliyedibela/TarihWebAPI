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

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetAllTradeRoutes
{
    public class GetAllTradeRoutesQueryHandler : BaseHandler, IRequestHandler<GetAllTradeRoutesQueryRequest, IList<GetAllTradeRoutesQueryResponse>>
    {
        public GetAllTradeRoutesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public async Task<IList<GetAllTradeRoutesQueryResponse>> Handle(GetAllTradeRoutesQueryRequest request, CancellationToken cancellationToken)
        {
            var tradeRoutes = await unitOfWork.GetReadRepository<TradeRoute>()
                .GetAllByPagingAsync(
                predicate: tr => tr.IsActive,
                include: tr => tr.Include(t => t.StartLocation).Include(t => t.EndLocation),
                orderBy: tr => tr.OrderBy(t => t.Name), 
               enableTracking: false);
               
            return tradeRoutes.Select(t => new GetAllTradeRoutesQueryResponse
            {
                Id = t.Id,
                Name = t.Name,
                AlternativeName = t.AlternativeName,
                StartYear = t.StartYear,
                EndYear = t.EndYear,
                Description = t.Description,
                MainGoods = t.MainGoods,
                StartLocationId = t.StartLocationId,
                EndLocationId = t.EndLocationId,
                RouteGeometryWkt = t.RouteGeometryWkt,
                Importance = t.Importance,
                MapUrl = t.MapUrl,
                WikipediaUrl = t.WikipediaUrl,
                StartLocation = t.StartLocation,
                EndLocation = t.EndLocation
            }).ToList();
        }
    }
}
