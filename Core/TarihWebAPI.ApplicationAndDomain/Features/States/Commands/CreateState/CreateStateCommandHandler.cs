using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.CreateState
{
    public class CreateStateCommandHandler : BaseHandler, IRequestHandler<CreateStateCommandRequest, Guid>
    {
        public CreateStateCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(CreateStateCommandRequest request, CancellationToken cancellationToken)
        {
            var state = new State
            {
                Name = request.Name,
                OfficialName = request.OfficialName,
                NameInOriginalLanguage = request.NameInOriginalLanguage,
                GovernmentType = request.GovernmentType,
                StartYear = request.StartYear,
                EndYear = request.EndYear,
                CapitalLocationId = request.CapitalLocationId,
                CapitalName = request.CapitalName,
                OfficialReligionId = request.OfficialReligionId,
                Currency = request.Currency,
                Description = request.Description,
                FlagUrl = request.FlagUrl,
                CoatOfArmsUrl = request.CoatOfArmsUrl,
                ColorCode = request.ColorCode,
                WikipediaUrl = request.WikipediaUrl,
                IsActive = true
            };

            await unitOfWork.GetWriteRepository<State>().AddAsync(state);
            await unitOfWork.SaveAsync(); 
            if (request.OfficialLanguages?.Any() == true)
            {
                var languages = request.OfficialLanguages.Select(l => new StateOfficialLanguage
                {
                    StateId = state.Id,
                    Language = l.Language,
                    IsPrimary = l.IsPrimary,
                    StartYear = l.StartYear,
                    EndYear = l.EndYear,
                    IsActive = true
                }).ToList();

                await unitOfWork.GetWriteRepository<StateOfficialLanguage>().AddRangeAsync(languages);
                await unitOfWork.SaveAsync();
            }

            return state.Id;
        }
    }
}