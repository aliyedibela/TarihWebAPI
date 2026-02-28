using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}