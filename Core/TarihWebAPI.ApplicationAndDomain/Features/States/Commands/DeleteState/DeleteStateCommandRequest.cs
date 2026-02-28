
using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.States.Commands.DeleteState
{
    public class DeleteStateCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}