using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetAllLocations
{
    public class GetAllLocationsQueryRequest : IRequest<IList<GetAllLocationsQueryResponse>>
    {

        public string? Type { get; set; }

        public bool? OnlyRoots { get; set; }
        public Guid? ParentLocationId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}