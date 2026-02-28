using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetReligionById
{
    public class GetReligionByIdQueryRequest : IRequest<GetReligionByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}