using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.CreateEra
{
    public class CreateEraCommandHandler : BaseHandler, IRequestHandler<CreateEraCommandRequest, Guid>
    {
        public CreateEraCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(CreateEraCommandRequest request, CancellationToken cancellationToken)
        {
            var era = new Era
            {
                Name = request.Name,
                NameInOriginalLanguage = request.NameInOriginalLanguage,
                StartYear = request.StartYear,
                EndYear = request.EndYear,
                Description = request.Description,
                MainCharacteristics = request.MainCharacteristics,
                ColorCode = request.ColorCode,
                WikipediaUrl = request.WikipediaUrl,
                IsActive = true
            };

            await unitOfWork.GetWriteRepository<Era>().AddAsync(era);
            await unitOfWork.SaveAsync();

            return era.Id;
        }
    }
}