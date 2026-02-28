using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Commands.DeleteReligion
{
    public class DeleteReligionCommandRequest : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}