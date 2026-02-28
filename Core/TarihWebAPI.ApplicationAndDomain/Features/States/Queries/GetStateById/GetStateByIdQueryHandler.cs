using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Queries.GetStateById
{
    public class GetStateByIdQueryHandler : BaseHandler, IRequestHandler<GetStateByIdQueryRequest, GetStateByIdQueryResponse>
    {
        public GetStateByIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<GetStateByIdQueryResponse> Handle(GetStateByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GetReadRepository<State>()
                .GetAsync(
                    predicate: s => s.Id == request.Id && s.IsActive,
                    include: query => query
                        .Include(s => s.OfficialReligion)
                        .Include(s => s.CapitalLocation)
                        .Include(s => s.OfficialLanguages)
                        .Include(s => s.Periods.Where(p => p.IsActive))
                        .Include(s => s.StateDynastyRelations.Where(r => r.IsActive))
                            .ThenInclude(r => r.Dynasty)
                        .Include(s => s.PopulationData.Where(p => p.IsActive)),
                    enableTracking: false
                );

            if (state is null)
                return null;

            return new GetStateByIdQueryResponse
            {
                Id = state.Id,
                Name = state.Name,
                OfficialName = state.OfficialName,
                NameInOriginalLanguage = state.NameInOriginalLanguage,
                GovernmentType = state.GovernmentType,
                StartYear = state.StartYear,
                EndYear = state.EndYear,
                CapitalLocationId = state.CapitalLocationId,
                CapitalName = state.CapitalLocation?.Name ?? state.CapitalName,
                OfficialReligionId = state.OfficialReligionId,
                OfficialReligionName = state.OfficialReligion?.Name,
                Currency = state.Currency,
                Description = state.Description,
                FlagUrl = state.FlagUrl,
                CoatOfArmsUrl = state.CoatOfArmsUrl,
                ColorCode = state.ColorCode,
                WikipediaUrl = state.WikipediaUrl,

                OfficialLanguages = state.OfficialLanguages?
                    .Where(l => l.IsActive)
                    .OrderByDescending(l => l.IsPrimary)
                    .Select(l => new StateLanguageDto
                    {
                        Language = l.Language,
                        IsPrimary = l.IsPrimary,
                        StartYear = l.StartYear,
                        EndYear = l.EndYear
                    }).ToList() ?? new(),

                Periods = state.Periods?
                    .OrderBy(p => p.OrderNumber ?? p.StartYear)
                    .Select(p => new StatePeriodDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        StartYear = p.StartYear,
                        EndYear = p.EndYear,
                        ColorCode = p.ColorCode,
                        OrderNumber = p.OrderNumber
                    }).ToList() ?? new(),

                Dynasties = state.StateDynastyRelations?
                    .OrderBy(r => r.StartYear)
                    .Select(r => new StateDynastyDto
                    {
                        DynastyId = r.DynastyId,
                        DynastyName = r.Dynasty?.Name,
                        StartYear = r.StartYear,
                        EndYear = r.EndYear
                    }).ToList() ?? new(),

                PopulationData = state.PopulationData?
                    .OrderBy(p => p.Year)
                    .Select(p => new StatePopulationDto
                    {
                        Year = p.Year,
                        Population = p.Population,
                        Source = p.Source
                    }).ToList() ?? new()
            };
        }
    }
}