using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Religions.Queries.GetAllReligions
{
    public class GetAllReligionsQueryRequest : IRequest<IList<GetAllReligionsQueryResponse>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}