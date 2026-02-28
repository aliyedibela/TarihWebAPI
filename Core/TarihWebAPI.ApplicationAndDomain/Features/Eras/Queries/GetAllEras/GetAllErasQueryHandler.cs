using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetAllEras
{
    public class GetAllErasQueryHandler : BaseHandler, IRequestHandler<GetAllErasQueryRequest, IList<GetAllErasQueryResponse>>
    {
        public GetAllErasQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<IList<GetAllErasQueryResponse>> Handle(GetAllErasQueryRequest request, CancellationToken cancellationToken)
        {
            var eras = await unitOfWork.GetReadRepository<Era>()
                .GetAllByPagingAsync(
                    predicate: e => e.IsActive &&
                        (!request.Year.HasValue ||
                            (e.StartYear <= request.Year &&
                             (!e.EndYear.HasValue || e.EndYear >= request.Year))),
                    include: query => query
                        .Include(e => e.Periods)
                        .Include(e => e.Events),
                    orderBy: q => q.OrderBy(e => e.StartYear),
                    currentPage: request.Page,
                    pageSize: request.PageSize,
                    enableTracking: false
                );

            return eras.Select(e => new GetAllErasQueryResponse
            {
                Id = e.Id,
                Name = e.Name,
                NameInOriginalLanguage = e.NameInOriginalLanguage,
                StartYear = e.StartYear,
                EndYear = e.EndYear,
                MainCharacteristics = e.MainCharacteristics,
                ColorCode = e.ColorCode,
                WikipediaUrl = e.WikipediaUrl,
                PeriodCount = e.Periods?.Count(p => p.IsActive) ?? 0,
                EventCount = e.Events?.Count(ev => ev.IsActive) ?? 0
            }).ToList();
        }
    }
}