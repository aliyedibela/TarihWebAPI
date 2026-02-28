using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetAllReligions
{
    public class GetAllReligionsQueryHandler : BaseHandler, IRequestHandler<GetAllReligionsQueryRequest, IList<GetAllReligionsQueryResponse>>
    {
        public GetAllReligionsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<IList<GetAllReligionsQueryResponse>> Handle(GetAllReligionsQueryRequest request, CancellationToken cancellationToken)
        {
            var religions = await unitOfWork.GetReadRepository<Religion>()
                .GetAllByPagingAsync(
                    predicate: r => r.IsActive,
                    include: query => query
                        .Include(r => r.FounderPerson)
                        .Include(r => r.FoundedLocation)
                        .Include(r => r.Sects),
                    orderBy: q => q.OrderBy(r => r.Name),
                    currentPage: request.Page,
                    pageSize: request.PageSize,
                    enableTracking: false
                );

            return religions.Select(r => new GetAllReligionsQueryResponse
            {
                Id = r.Id,
                Name = r.Name,
                NameInOriginalLanguage = r.NameInOriginalLanguage,
                ShortName = r.ShortName,
                MainBranch = r.MainBranch,
                FoundedYear = r.FoundedYear,
                FoundedBy = r.FounderPerson?.FullName ?? r.FoundedBy,
                FoundedLocationName = r.FoundedLocation?.Name ?? r.FoundedLocationName,
                ApproximateFollowers = r.ApproximateFollowers,
                IconUrl = r.IconUrl,
                ColorCode = r.ColorCode,
                SectCount = r.Sects?.Count(s => s.IsActive) ?? 0
            }).ToList();
        }
    }
}