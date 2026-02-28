using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.UpdateState
{
    public class UpdateStateCommandHandler : BaseHandler, IRequestHandler<UpdateStateCommandRequest, Guid>
    {
        public UpdateStateCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(UpdateStateCommandRequest request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GetReadRepository<State>()
                .GetAsync(
                    predicate: s => s.Id == request.Id && s.IsActive,
                    enableTracking: true 
                );

            if (state is null)
                throw new Exception($"Devlet bulunamadı. Id: {request.Id}");

            state.Name = request.Name;
            state.OfficialName = request.OfficialName;
            state.NameInOriginalLanguage = request.NameInOriginalLanguage;
            state.GovernmentType = request.GovernmentType;
            state.StartYear = request.StartYear;
            state.EndYear = request.EndYear;
            state.CapitalLocationId = request.CapitalLocationId;
            state.CapitalName = request.CapitalName;
            state.OfficialReligionId = request.OfficialReligionId;
            state.Currency = request.Currency;
            state.Description = request.Description;
            state.FlagUrl = request.FlagUrl;
            state.CoatOfArmsUrl = request.CoatOfArmsUrl;
            state.ColorCode = request.ColorCode;
            state.WikipediaUrl = request.WikipediaUrl;

            await unitOfWork.GetWriteRepository<State>().UpdateAsync(state);
            await unitOfWork.SaveAsync();

            return state.Id;
        }
    }
}