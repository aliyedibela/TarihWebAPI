using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetAllLocations
{
    public class GetAllLocationsQueryHandler : BaseHandler, IRequestHandler<GetAllLocationsQueryRequest, IList<GetAllLocationsQueryResponse>>
    {
        public GetAllLocationsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<IList<GetAllLocationsQueryResponse>> Handle(GetAllLocationsQueryRequest request, CancellationToken cancellationToken)
        {
            var locations = await unitOfWork.GetReadRepository<Location>()
                .GetAllByPagingAsync(
                    predicate: l => l.IsActive &&
                        (string.IsNullOrEmpty(request.Type) || l.Type == request.Type) &&
                        (!request.OnlyRoots.HasValue || !request.OnlyRoots.Value || l.ParentLocationId == null) &&
                        (!request.ParentLocationId.HasValue || l.ParentLocationId == request.ParentLocationId),
                    include: query => query
                        .Include(l => l.ParentLocation)
                        .Include(l => l.SubLocations),
                    orderBy: q => q.OrderBy(l => l.Name),
                    currentPage: request.Page,
                    pageSize: request.PageSize,
                    enableTracking: false
                );

            return locations.Select(l => new GetAllLocationsQueryResponse
            {
                Id = l.Id,
                Name = l.Name,
                NameInOriginalLanguage = l.NameInOriginalLanguage,
                Type = l.Type,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                ModernName = l.ModernName,
                Country = l.Country,
                Region = l.Region,
                ParentLocationId = l.ParentLocationId,
                ParentLocationName = l.ParentLocation?.Name,
                SubLocationCount = l.SubLocations?.Count(s => s.IsActive) ?? 0
            }).ToList();
        }
    }
}