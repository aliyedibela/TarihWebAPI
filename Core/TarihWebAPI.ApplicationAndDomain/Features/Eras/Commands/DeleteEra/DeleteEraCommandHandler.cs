using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.DeleteEra
{
    public class DeleteEraCommandHandler : BaseHandler, IRequestHandler<DeleteEraCommandRequest, Unit>
    {
        public DeleteEraCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteEraCommandRequest request, CancellationToken cancellationToken)
        {
            var era = await unitOfWork.GetReadRepository<Era>()
                .GetAsync(
                    predicate: e => e.Id == request.Id && e.IsActive,
                    enableTracking: true
                );

            if (era is null)
                throw new Exception($"Çağ bulunamadı veya zaten silinmiş. Id: {request.Id}");

            era.IsActive = false;

            await unitOfWork.GetWriteRepository<Era>().UpdateAsync(era);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}