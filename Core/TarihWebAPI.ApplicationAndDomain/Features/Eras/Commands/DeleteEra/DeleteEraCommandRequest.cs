using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Commands.DeleteEra
{
    public class DeleteEraCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}