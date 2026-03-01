using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetAllTradeRoutes
{
    public class GetAllTradeRoutesQueryRequest : IRequest<IList<GetAllTradeRoutesQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
