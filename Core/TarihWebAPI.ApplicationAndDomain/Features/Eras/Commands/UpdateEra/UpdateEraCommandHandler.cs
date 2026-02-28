using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.UpdateEra
{
    public class UpdateEraCommandHandler : BaseHandler, IRequestHandler<UpdateEraCommandRequest, Guid>
    {
        public UpdateEraCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(UpdateEraCommandRequest request, CancellationToken cancellationToken)
        {
            var era = await unitOfWork.GetReadRepository<Era>()
                .GetAsync(
                    predicate: e => e.Id == request.Id && e.IsActive,
                    enableTracking: true
                );

            if (era is null)
                throw new Exception($"Çağ bulunamadı. Id: {request.Id}");

            era.Name = request.Name;
            era.NameInOriginalLanguage = request.NameInOriginalLanguage;
            era.StartYear = request.StartYear;
            era.EndYear = request.EndYear;
            era.Description = request.Description;
            era.MainCharacteristics = request.MainCharacteristics;
            era.ColorCode = request.ColorCode;
            era.WikipediaUrl = request.WikipediaUrl;

            await unitOfWork.GetWriteRepository<Era>().UpdateAsync(era);
            await unitOfWork.SaveAsync();

            return era.Id;
        }
    }
}