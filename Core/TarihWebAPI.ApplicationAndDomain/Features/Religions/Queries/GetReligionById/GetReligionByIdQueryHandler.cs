using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetReligionById
{
    public class GetReligionByIdQueryHandler : BaseHandler, IRequestHandler<GetReligionByIdQueryRequest, GetReligionByIdQueryResponse>
    {
        public GetReligionByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<GetReligionByIdQueryResponse> Handle(GetReligionByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var religion = await unitOfWork.GetReadRepository<Religion>()
                .GetAsync(
                    predicate: r => r.Id == request.Id && r.IsActive,
                    include: query => query
                        .Include(r => r.FounderPerson)
                        .Include(r => r.FoundedLocation)
                        .Include(r => r.HolyBooks.Where(hb => hb.IsActive))
                        .Include(r => r.HolyCities.Where(hc => hc.IsActive))
                            .ThenInclude(hc => hc.Location)
                        .Include(r => r.SubBranches.Where(sb => sb.IsActive))
                        .Include(r => r.Sects.Where(s => s.IsActive)),
                    enableTracking: false
                );

            if (religion is null)
                return null;

            return new GetReligionByIdQueryResponse
            {
                Id = religion.Id,
                Name = religion.Name,
                NameInOriginalLanguage = religion.NameInOriginalLanguage,
                ShortName = religion.ShortName,
                Description = religion.Description,
                MainBranch = religion.MainBranch,
                FoundedYear = religion.FoundedYear,
                FounderPersonId = religion.FounderPersonId,
                FounderPersonName = religion.FounderPerson?.FullName ?? religion.FoundedBy,
                FoundedLocationId = religion.FoundedLocationId,
                FoundedLocationName = religion.FoundedLocation?.Name ?? religion.FoundedLocationName,
                ApproximateFollowers = religion.ApproximateFollowers,
                IconUrl = religion.IconUrl,
                ColorCode = religion.ColorCode,
                WikipediaUrl = religion.WikipediaUrl,

                HolyBooks = religion.HolyBooks?
                    .Select(hb => new ReligionHolyBookDto
                    {
                        Id = hb.Id,
                        Name = hb.Name,
                        OriginalName = hb.OriginalName,
                        Language = hb.Language,
                        Description = hb.Description,
                        WikipediaUrl = hb.WikipediaUrl
                    }).ToList() ?? new(),

                HolyCities = religion.HolyCities?
                    .Select(hc => new ReligionHolyCityDto
                    {
                        Id = hc.Id,
                        CityName = hc.CityName,
                        Significance = hc.Significance,
                        Description = hc.Description,
                        LocationId = hc.LocationId,
                        Latitude = hc.Location?.Latitude,
                        Longitude = hc.Location?.Longitude
                    }).ToList() ?? new(),

                SubBranches = religion.SubBranches?
                    .Select(sb => new ReligionSubBranchDto
                    {
                        Id = sb.Id,
                        Name = sb.Name,
                        NameInOriginalLanguage = sb.NameInOriginalLanguage,
                        Description = sb.Description,
                        FoundedYear = sb.FoundedYear,
                        WikipediaUrl = sb.WikipediaUrl
                    }).ToList() ?? new(),

                Sects = religion.Sects?
                    .OrderBy(s => s.Name)
                    .Select(s => new ReligionSectDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        NameInOriginalLanguage = s.NameInOriginalLanguage,
                        Description = s.Description,
                        FoundedBy = s.FoundedBy,
                        WikipediaUrl = s.WikipediaUrl
                    }).ToList() ?? new()
            };
        }
    }
}