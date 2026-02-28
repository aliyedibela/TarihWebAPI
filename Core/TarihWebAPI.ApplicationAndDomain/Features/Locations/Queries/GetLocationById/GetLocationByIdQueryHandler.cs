using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Queries.GetLocationById
{
    public class GetLocationByIdQueryHandler : BaseHandler, IRequestHandler<GetLocationByIdQueryRequest, GetLocationByIdQueryResponse>
    {
        public GetLocationByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<GetLocationByIdQueryResponse> Handle(GetLocationByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork.GetReadRepository<Location>()
                .GetAsync(
                    predicate: l => l.Id == request.Id && l.IsActive,
                    include: query => query
                        .Include(l => l.ParentLocation)
                        .Include(l => l.SubLocations.Where(s => s.IsActive))
                        .Include(l => l.Events.Where(e => e.IsActive))
                        .Include(l => l.BornPersons.Where(p => p.IsActive))
                        .Include(l => l.DiedPersons.Where(p => p.IsActive)),
                    enableTracking: false
                );

            if (location is null)
                return null;

            return new GetLocationByIdQueryResponse
            {
                Id = location.Id,
                Name = location.Name,
                NameInOriginalLanguage = location.NameInOriginalLanguage,
                Type = location.Type,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                ModernName = location.ModernName,
                Country = location.Country,
                Region = location.Region,
                Description = location.Description,
                ImageUrl = location.ImageUrl,
                WikipediaUrl = location.WikipediaUrl,
                ParentLocationId = location.ParentLocationId,
                ParentLocationName = location.ParentLocation?.Name,

                SubLocations = location.SubLocations?
                    .Select(s => new SubLocationDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Type = s.Type,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude
                    }).ToList() ?? new(),

                Events = location.Events?
                    .OrderByDescending(e => e.Importance)
                    .Select(e => new LocationEventDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        StartYear = e.StartYear,
                        EndYear = e.EndYear,
                        EventType = e.EventType?.ToString(),
                        Importance = e.Importance
                    }).ToList() ?? new(),

                BornPersons = location.BornPersons?
                    .Select(p => new LocationPersonDto
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        BirthYear = p.BirthYear,
                        DeathYear = p.DeathYear,
                        ImageUrl = p.ImageUrl
                    }).ToList() ?? new(),

                DiedPersons = location.DiedPersons?
                    .Select(p => new LocationPersonDto
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        BirthYear = p.BirthYear,
                        DeathYear = p.DeathYear,
                        ImageUrl = p.ImageUrl
                    }).ToList() ?? new()
            };
        }
    }
}