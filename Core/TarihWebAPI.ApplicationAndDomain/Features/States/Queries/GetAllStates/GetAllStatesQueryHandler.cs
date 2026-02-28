using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetAllStates
{
    public class GetAllStatesQueryHandler : BaseHandler, IRequestHandler<GetAllStatesQueryRequest, IList<GetAllStatesQueryResponse>>
    {
        public GetAllStatesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<IList<GetAllStatesQueryResponse>> Handle(GetAllStatesQueryRequest request, CancellationToken cancellationToken)
        {
            var states = await unitOfWork.GetReadRepository<State>()
                .GetAllByPagingAsync(
                    predicate: s => s.IsActive &&
                        (!request.Year.HasValue ||
                            ((!s.StartYear.HasValue || s.StartYear <= request.Year) &&
                             (!s.EndYear.HasValue || s.EndYear >= request.Year))),
                    include: query => query
                        .Include(s => s.OfficialReligion)
                        .Include(s => s.OfficialLanguages),
                    orderBy: q => q.OrderBy(s => s.Name),
                    currentPage: request.Page,
                    pageSize: request.PageSize,
                    enableTracking: false
                );

            return states.Select(s => new GetAllStatesQueryResponse
            {
                Id = s.Id,
                Name = s.Name,
                OfficialName = s.OfficialName,
                NameInOriginalLanguage = s.NameInOriginalLanguage,
                GovernmentType = s.GovernmentType,
                StartYear = s.StartYear,
                EndYear = s.EndYear,
                ColorCode = s.ColorCode,
                FlagUrl = s.FlagUrl,
                CapitalName = s.CapitalLocation?.Name ?? s.CapitalName,
                OfficialReligionName = s.OfficialReligion?.Name,
                OfficialLanguages = s.OfficialLanguages?
                    .Where(l => l.IsActive)
                    .OrderByDescending(l => l.IsPrimary)
                    .Select(l => l.Language)
                    .ToList() ?? new List<string>()
            }).ToList();
        }
    }
}