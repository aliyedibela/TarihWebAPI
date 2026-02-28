using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommandHandler : BaseHandler, IRequestHandler<DeleteLocationCommandRequest, Unit>
    {
        public DeleteLocationCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteLocationCommandRequest request, CancellationToken cancellationToken)
        {
            var location = await unitOfWork.GetReadRepository<Location>()
                .GetAsync(
                    predicate: l => l.Id == request.Id && l.IsActive,
                    enableTracking: true
                );

            if (location is null)
                throw new Exception($"Lokasyon bulunamadı veya zaten silinmiş. Id: {request.Id}");

            location.IsActive = false;

            await unitOfWork.GetWriteRepository<Location>().UpdateAsync(location);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}