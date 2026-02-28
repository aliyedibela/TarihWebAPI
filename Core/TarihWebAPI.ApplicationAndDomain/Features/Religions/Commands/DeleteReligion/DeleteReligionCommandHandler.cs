using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.DeleteReligion
{
    public class DeleteReligionCommandHandler : BaseHandler, IRequestHandler<DeleteReligionCommandRequest, Unit>
    {
        public DeleteReligionCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteReligionCommandRequest request, CancellationToken cancellationToken)
        {
            var religion = await unitOfWork.GetReadRepository<Religion>()
                .GetAsync(
                    predicate: r => r.Id == request.Id && r.IsActive,
                    enableTracking: true
                );

            if (religion is null)
                throw new Exception($"Din bulunamadı veya zaten silinmiş. Id: {request.Id}");

            religion.IsActive = false;

            await unitOfWork.GetWriteRepository<Religion>().UpdateAsync(religion);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}