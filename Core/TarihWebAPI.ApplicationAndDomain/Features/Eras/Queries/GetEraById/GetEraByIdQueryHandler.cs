using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetEraById
{
    public class GetEraByIdQueryHandler : BaseHandler, IRequestHandler<GetEraByIdQueryRequest, GetEraByIdQueryResponse>
    {
        public GetEraByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<GetEraByIdQueryResponse> Handle(GetEraByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var era = await unitOfWork.GetReadRepository<Era>()
                .GetAsync(
                    predicate: e => e.Id == request.Id && e.IsActive,
                    include: query => query
                        .Include(e => e.Periods.Where(p => p.IsActive))
                            .ThenInclude(p => p.State)
                        .Include(e => e.Events.Where(ev => ev.IsActive))
                        .Include(e => e.Persons.Where(p => p.IsActive)),
                    enableTracking: false
                );

            if (era is null)
                return null;

            return new GetEraByIdQueryResponse
            {
                Id = era.Id,
                Name = era.Name,
                NameInOriginalLanguage = era.NameInOriginalLanguage,
                StartYear = era.StartYear,
                EndYear = era.EndYear,
                Description = era.Description,
                MainCharacteristics = era.MainCharacteristics,
                ColorCode = era.ColorCode,
                WikipediaUrl = era.WikipediaUrl,

                Periods = era.Periods?
                    .OrderBy(p => p.StartYear)
                    .Select(p => new EraPeriodDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        StateName = p.State?.Name,
                        StartYear = p.StartYear,
                        EndYear = p.EndYear,
                        ColorCode = p.ColorCode
                    }).ToList() ?? new(),

                Events = era.Events?
                    .OrderByDescending(e => e.Importance)
                    .ThenBy(e => e.StartYear)
                    .Select(e => new EraEventDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        StartYear = e.StartYear,
                        EndYear = e.EndYear,
                        EventType = e.EventType?.ToString(),
                        Importance = e.Importance
                    }).ToList() ?? new(),

                Persons = era.Persons?
                    .OrderBy(p => p.BirthYear)
                    .Select(p => new EraPersonDto
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