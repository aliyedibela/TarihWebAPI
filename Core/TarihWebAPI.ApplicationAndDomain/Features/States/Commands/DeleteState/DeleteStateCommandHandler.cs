using MediatR;
using Microsoft.AspNetCore.Http;
using TarihWebAPI.ApplicationAndDomain.Bases;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Interfaces.UnitOfWorks;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.DeleteState
{
    public class DeleteStateCommandHandler : BaseHandler, IRequestHandler<DeleteStateCommandRequest, Unit>
    {
        public DeleteStateCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteStateCommandRequest request, CancellationToken cancellationToken)
        {
            var state = await unitOfWork.GetReadRepository<State>()
                .GetAsync(
                    predicate: s => s.Id == request.Id && s.IsActive,
                    enableTracking: true
                );

            if (state is null)
                throw new Exception($"Devlet bulunamadı veya zaten silinmiş. Id: {request.Id}");
            state.IsActive = false;
            await unitOfWork.GetWriteRepository<State>().UpdateAsync(state);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}