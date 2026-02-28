using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandHandler : BaseHandler, IRequestHandler<CreateLocationCommandRequest, Guid>
    {
        public CreateLocationCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Guid> Handle(CreateLocationCommandRequest request, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                Name = request.Name,
                NameInOriginalLanguage = request.NameInOriginalLanguage,
                Type = request.Type,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                ModernName = request.ModernName,
                Country = request.Country,
                Region = request.Region,
                ParentLocationId = request.ParentLocationId,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                WikipediaUrl = request.WikipediaUrl,
                IsActive = true
            };

            await unitOfWork.GetWriteRepository<Location>().AddAsync(location);
            await unitOfWork.SaveAsync();

            return location.Id;
        }
    }
}