using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQueryRequest : IRequest<GetLocationByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}