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

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.UpdateTradeRoute
{
    public class UpdateTradeRouteCommandHandler : BaseHandler, IRequestHandler<UpdateTradeRouteCommandRequest, Guid>
    {
        public UpdateTradeRouteCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Guid> Handle(UpdateTradeRouteCommandRequest request, CancellationToken cancellationToken)
        {
            var tradeRoute = unitOfWork.GetReadRepository<TradeRoute>().GetAsync(x => x.Id == request.Id).Result;

            if (tradeRoute == null) {
                throw new Exception("Bu Id'de bir yol rotası yok.");

                } 

            tradeRoute.Name = request.Name;
            tradeRoute.Description = request.Description;
            tradeRoute.AlternativeName = request.AlternativeName;
            tradeRoute.StartYear = request.StartYear;
            tradeRoute.EndYear = request.EndYear;
            tradeRoute.MainGoods = request.MainGoods;
            tradeRoute.StartLocationId = request.StartLocationId;
            tradeRoute.EndLocationId = request.EndLocationId;
            tradeRoute.RouteGeometryWkt = request.RouteGeometryWkt;
            tradeRoute.Importance = request.Importance;
            tradeRoute.MapUrl = request.MapUrl;
            tradeRoute.WikipediaUrl = request.WikipediaUrl;
                
           await unitOfWork.GetWriteRepository<TradeRoute>().UpdateAsync(tradeRoute);
          await  unitOfWork.SaveAsync();

            return tradeRoute.Id;
        }
        }
    }

