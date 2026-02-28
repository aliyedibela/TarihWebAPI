using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.UpdateReligion
{
    public class UpdateReligionCommandHandler : BaseHandler, IRequestHandler<UpdateReligionCommandRequest, Guid>
    {
        public UpdateReligionCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(UpdateReligionCommandRequest request, CancellationToken cancellationToken)
        {
            var religion = await unitOfWork.GetReadRepository<Religion>()
                .GetAsync(
                    predicate: r => r.Id == request.Id && r.IsActive,
                    enableTracking: true
                );

            if (religion is null)
                throw new Exception($"Din bulunamadı. Id: {request.Id}");

            religion.Name = request.Name;
            religion.NameInOriginalLanguage = request.NameInOriginalLanguage;
            religion.ShortName = request.ShortName;
            religion.Description = request.Description;
            religion.MainBranch = request.MainBranch;
            religion.FoundedYear = request.FoundedYear;
            religion.FounderPersonId = request.FounderPersonId;
            religion.FoundedBy = request.FoundedBy;
            religion.FoundedLocationId = request.FoundedLocationId;
            religion.FoundedLocationName = request.FoundedLocationName;
            religion.ApproximateFollowers = request.ApproximateFollowers;
            religion.IconUrl = request.IconUrl;
            religion.ColorCode = request.ColorCode;
            religion.WikipediaUrl = request.WikipediaUrl;

            await unitOfWork.GetWriteRepository<Religion>().UpdateAsync(religion);
            await unitOfWork.SaveAsync();

            return religion.Id;
        }
    }
}