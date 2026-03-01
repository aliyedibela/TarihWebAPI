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

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.DeleteTradeRoute
{
    public class DeleteTradeRouteCommandHandler : BaseHandler, IRequestHandler<DeleteTradeRouteCommandRequest, Guid>
    {
        public DeleteTradeRouteCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Guid> Handle(DeleteTradeRouteCommandRequest request, CancellationToken cancellationToken)
        {
            var tradeRoute = unitOfWork.GetReadRepository<TradeRoute>().GetAsync(x => x.Id == request.Id).Result;
            if (tradeRoute == null) { 
            throw new Exception("Trade route not found");
            }
            else
            {
                tradeRoute.IsActive = false;
                await unitOfWork.GetWriteRepository<TradeRoute>().UpdateAsync(tradeRoute);
                await unitOfWork.SaveAsync();

                return tradeRoute.Id;
            }
        }
    }
}
