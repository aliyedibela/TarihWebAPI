using MediatR;

namespace TarihWebAPI.ApplicationAndDomain.Features.Eras.Queries.GetAllEras
{
    public class GetAllErasQueryRequest : IRequest<IList<GetAllErasQueryResponse>>
    {
        public int? Year { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}