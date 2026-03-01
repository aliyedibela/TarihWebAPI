using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Commands.DeleteTradeRoute
{
    public class DeleteTradeRouteCommandRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
