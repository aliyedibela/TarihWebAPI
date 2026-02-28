using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : BaseHandler, IRequestHandler<UpdateLocationCommandRequest, Guid>
    {
        public UpdateLocationCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(UpdateLocationCommandRequest request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork.GetReadRepository<Location>()
                .GetAsync(
                    predicate: l => l.Id == request.Id && l.IsActive,
                    enableTracking: true
                );

            if (location is null)
                throw new Exception($"Lokasyon bulunamadı. Id: {request.Id}");

            location.Name = request.Name;
            location.NameInOriginalLanguage = request.NameInOriginalLanguage;
            location.Type = request.Type;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            location.ModernName = request.ModernName;
            location.Country = request.Country;
            location.Region = request.Region;
            location.ParentLocationId = request.ParentLocationId;
            location.Description = request.Description;
            location.ImageUrl = request.ImageUrl;
            location.WikipediaUrl = request.WikipediaUrl;

            await unitOfWork.GetWriteRepository<Location>().UpdateAsync(location);
            await unitOfWork.SaveAsync();

            return location.Id;
        }
    }
}