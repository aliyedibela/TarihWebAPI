using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.TradeRoutes.Queries.GetTradeRouteById
{
    public class GetTradeRouteByIdQueryRequest : IRequest<GetTradeRouteByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
